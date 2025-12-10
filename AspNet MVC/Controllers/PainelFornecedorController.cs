using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;

namespace AspNet_MVC.Controllers;

[Authorize(Roles = "Fornecedor")]
public class PainelFornecedorController : Controller
{
    private readonly AppDbContext _context;

    public PainelFornecedorController(AppDbContext context)
    {
        _context = context;
    }

    // Lista as cotações disponíveis para o fornecedor logado
    public async Task<IActionResult> Index()
    {
        int fornecedorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var cotacoes = await _context.Cotacoes
            .Where(c =>
                _context.CotacaoParticipantes.Any(p =>
                    p.CotacaoId == c.Id && p.FornecedorId == fornecedorId))
            .OrderByDescending(c => c.DataAbertura)
            .ToListAsync();

        return View(cotacoes);
    }

    // GET: fornecedor lança/edita preços de uma cotação
    public async Task<IActionResult> Responder(int id)
    {
        int fornecedorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var cotacao = await _context.Cotacoes
            .Include(c => c.Itens)
                .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cotacao == null)
            return NotFound();

        // RF06 – validar prazo e status
        if (cotacao.DataFechamento < DateTime.Now || cotacao.Status != "Aberta")
        {
            TempData["Erro"] = "Esta cotação não está mais disponível para resposta.";
            return RedirectToAction(nameof(Index));
        }

        // garante que o fornecedor participa dessa cotação
        bool participante = await _context.CotacaoParticipantes
            .AnyAsync(p => p.CotacaoId == id && p.FornecedorId == fornecedorId);

        if (!participante)
            return Forbid();

        // preços já lançados (se houver)
        var precosExistentes = await _context.CotacaoPrecos
            .Where(cp => cp.FornecedorId == fornecedorId &&
                         cp.CotacaoItem.CotacaoId == id)
            .ToListAsync();

        var vm = new CotacaoRespostaViewModel
        {
            CotacaoId = cotacao.Id,
            DescricaoCotacao = cotacao.Descricao,
            DataFechamento = cotacao.DataFechamento,
            Itens = cotacao.Itens.Select(item =>
            {
                var precoItem = precosExistentes
                    .FirstOrDefault(p => p.CotacaoItemId == item.Id);

                return new CotacaoRespostaItemViewModel
                {
                    CotacaoItemId = item.Id,
                    ProdutoNome = item.Produto.Nome,
                    UnidadeMedida = item.Produto.UnidadeMedida ?? "",
                    Quantidade = item.Quantidade,
                    PrecoUnitario = precoItem?.PrecoUnitario
                };
            }).ToList()
        };

        return View(vm);
    }

    // POST: fornecedor envia os preços
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Responder(CotacaoRespostaViewModel model)
    {
        int fornecedorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var cotacao = await _context.Cotacoes
            .FirstOrDefaultAsync(c => c.Id == model.CotacaoId);

        if (cotacao == null)
            return NotFound();

        // RF06 – validar prazo de novo no POST
        if (cotacao.DataFechamento < DateTime.Now || cotacao.Status != "Aberta")
        {
            TempData["Erro"] = "Prazo encerrado. Não é mais possível enviar ou alterar valores.";
            return RedirectToAction(nameof(Index));
        }

        if (model.Itens == null || !model.Itens.Any())
        {
            TempData["Erro"] = "Nenhum item encontrado para resposta.";
            return RedirectToAction(nameof(Index));
        }

        foreach (var item in model.Itens)
        {
            if (!item.PrecoUnitario.HasValue || item.PrecoUnitario <= 0)
                continue; // ignora itens sem valor

            var precoExistente = await _context.CotacaoPrecos
                .FirstOrDefaultAsync(cp =>
                    cp.CotacaoItemId == item.CotacaoItemId &&
                    cp.FornecedorId == fornecedorId);

            if (precoExistente == null)
            {
                // ⬅️ AQUI ERA O ERRO — agora usamos só CotacaoPreco
                var novo = new CotacaoPreco
                {
                    CotacaoItemId = item.CotacaoItemId,
                    FornecedorId = fornecedorId,
                    PrecoUnitario = item.PrecoUnitario.Value,
                    DataResposta = DateTime.Now
                };
                _context.CotacaoPrecos.Add(novo);
            }
            else
            {
                precoExistente.PrecoUnitario = item.PrecoUnitario.Value;
                precoExistente.DataResposta = DateTime.Now;
                _context.CotacaoPrecos.Update(precoExistente);
            }
        }

        await _context.SaveChangesAsync();

        TempData["Sucesso"] = "Proposta enviada/atualizada com sucesso!";
        return RedirectToAction(nameof(Index));
    }
}

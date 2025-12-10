using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Entidades;
using Models.ViewModel;

namespace AspNet_MVC.Controllers;

[Authorize(Roles = "Admin")]
public class CotacoesController : Controller
{
    private readonly AppDbContext _context;

    public CotacoesController(AppDbContext context)
    {
        _context = context;
    }

    // RF03 – Listagem de Cotações
    public async Task<IActionResult> Index()
    {
        var cotacoes = await _context.Cotacoes
            .Include(c => c.Itens)
            .ToListAsync();

        return View(cotacoes);
    }

    // GET: Criar Cotação
    public async Task<IActionResult> Create()
    {
        ViewBag.Produtos = await _context.Produtos.ToListAsync();
        ViewBag.Fornecedores = await _context.Fornecedores.ToListAsync();

        var modelo = new Cotacao
        {
            DataFechamento = DateTime.Today.AddDays(7) // prazo padrão de 7 dias
        };

        return View(modelo);
    }

    // POST: Criar Cotação
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        Cotacao model,
        List<int> produtosSelecionados,
        List<int> fornecedoresSelecionados)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Produtos = await _context.Produtos.ToListAsync();
            ViewBag.Fornecedores = await _context.Fornecedores.ToListAsync();
            return View(model);
        }

        // RF03: cria a cotação com prazo
        model.DataAbertura = DateTime.Now;
        model.Status = "Aberta";

        _context.Cotacoes.Add(model);
        await _context.SaveChangesAsync();

        // RF03: cria itens da cotação (por enquanto quantidade = 1)
        if (produtosSelecionados != null && produtosSelecionados.Any())
        {
            foreach (var produtoId in produtosSelecionados.Distinct())
            {
                var item = new CotacaoItem
                {
                    CotacaoId = model.Id,
                    ProdutoId = produtoId,
                    Quantidade = 1,        // podemos melhorar isso depois
                    Observacao = null
                };

                _context.CotacaoItens.Add(item);
            }
        }

        // RF03: define fornecedores participantes
        if (fornecedoresSelecionados != null && fornecedoresSelecionados.Any())
        {
            foreach (var fornecedorId in fornecedoresSelecionados.Distinct())
            {
                var participante = new CotacaoParticipante
                {
                    CotacaoId = model.Id,
                    FornecedorId = fornecedorId
                };

                _context.CotacaoParticipantes.Add(participante);
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Resultado(int id)
    {
        var cotacao = await _context.Cotacoes
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cotacao == null)
            return NotFound();

        // RF07 – calcular total por fornecedor
        var resultados = await _context.CotacaoPrecos
            .Include(cp => cp.CotacaoItem)
            .Include(cp => cp.Fornecedor)
            .Where(cp => cp.CotacaoItem.CotacaoId == id)
            .GroupBy(cp => cp.Fornecedor)
            .Select(g => new CotacaoResultadoFornecedorViewModel
            {
                FornecedorId = g.Key.Id,
                FornecedorNome = g.Key.NomeFantasia,
                ValorTotal = g.Sum(x => x.PrecoUnitario * x.CotacaoItem.Quantidade)
            })
            .OrderBy(r => r.ValorTotal)
            .ToListAsync();

        var vm = new CotacaoResultadoViewModel
        {
            CotacaoId = cotacao.Id,
            DescricaoCotacao = cotacao.Descricao,
            DataFechamento = cotacao.DataFechamento,
            Fornecedores = resultados
        };

        // Opcional: marcar como processada após o fim do prazo
        if (cotacao.DataFechamento < DateTime.Now && cotacao.Status == "Aberta")
        {
            cotacao.Status = "Processada";
            _context.Cotacoes.Update(cotacao);
            await _context.SaveChangesAsync();
        }

        return View(vm);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models.Data;
using Models.Entidades;

namespace AspNet_MVC.Controllers;

[Authorize(Roles = "Admin")]
public class FornecedoresController : Controller
{
    private readonly AppDbContext _context;

    public FornecedoresController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var fornecedores = await _context.Fornecedores.ToListAsync();
        return View(fornecedores);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Fornecedor model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // 🔐 Gerar hash da senha antes de salvar
        var hasher = new PasswordHasher<Fornecedor>();
        model.SenhaHash = hasher.HashPassword(model, model.SenhaHash);

        _context.Fornecedores.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(id);
        if (fornecedor == null) return NotFound();

        // Não queremos mostrar o hash na tela
        fornecedor.SenhaHash = string.Empty;

        return View(fornecedor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Fornecedor model)
    {
        if (id != model.Id) return NotFound();

        // SenhaHash é [Required], mas no Edit ela é OPCIONAL
        ModelState.Remove(nameof(Fornecedor.SenhaHash));

        if (!ModelState.IsValid)
            return View(model);

        var fornecedor = await _context.Fornecedores.FindAsync(id);
        if (fornecedor == null) return NotFound();

        // Atualiza apenas os campos permitidos
        fornecedor.NomeFantasia    = model.NomeFantasia;
        fornecedor.RazaoSocial     = model.RazaoSocial;
        fornecedor.CNPJ            = model.CNPJ;           // se não quiser alterar CNPJ, você pode remover essa linha
        fornecedor.Email           = model.Email;
        fornecedor.Telefone        = model.Telefone;
        fornecedor.NomeResponsavel = model.NomeResponsavel;

        // 🔐 Se o usuário informou uma nova senha, gera novo hash
        if (!string.IsNullOrWhiteSpace(model.SenhaHash))
        {
            var hasher = new PasswordHasher<Fornecedor>();
            fornecedor.SenhaHash = hasher.HashPassword(fornecedor, model.SenhaHash);
        }

        _context.Update(fornecedor);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(id);
        if (fornecedor == null) return NotFound();
        return View(fornecedor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var fornecedor = await _context.Fornecedores.FindAsync(id);
        if (fornecedor != null)
        {
            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}

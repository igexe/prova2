using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Models.Data;
using Models.ViewModel;

namespace AspNet_MVC.Controllers;

public class AutenticacaoController : Controller
{
    private readonly AppDbContext _context;

    public AutenticacaoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    // Função auxiliar para calcular SHA-256 em HEX (igual está no banco do Admin)
    private static string CalcularSha256Hex(string senha)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(senha));
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("X2")); // maiúsculo, ex: 8D96...
        }
        return sb.ToString();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var hasher = new PasswordHasher<string>();

        string role = model.TipoUsuario;
        int id;
        string nomeExibicao;

        if (model.TipoUsuario == "Admin")
        {
            // ADMIN VALIDA COM SHA-256 (HEX)
            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (usuario is null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(model);
            }

            var hashDigitado = CalcularSha256Hex(model.Senha);

            if (!string.Equals(usuario.SenhaHash, hashDigitado, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(model);
            }

            id = usuario.Id;
            nomeExibicao = usuario.Nome;
            role = "Admin";
        }
        else // Fornecedor
        {
            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(f => f.Email == model.Email);

            if (fornecedor is null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(model);
            }

            var result = hasher.VerifyHashedPassword(
                null,
                fornecedor.SenhaHash,
                model.Senha
            );

            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View(model);
            }

            id = fornecedor.Id;
            nomeExibicao = fornecedor.NomeFantasia;
            role = "Fornecedor";
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Name, nomeExibicao),
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(ClaimTypes.Role, role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}

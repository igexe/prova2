using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Tipo de Usuário")]
    public string TipoUsuario { get; set; } = "Admin"; // Admin ou Fornecedor
}

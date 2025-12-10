using System.ComponentModel.DataAnnotations;

namespace Models.Entidades;

public class Usuario
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(255)]
    public string SenhaHash { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string TipoUsuario { get; set; } = "Admin"; // Admin, Outro...
}

using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel;

public class FornecedorViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nome Fantasia")]
    public string NomeFantasia { get; set; } = string.Empty;

    [Display(Name = "Razão Social")]
    public string? RazaoSocial { get; set; }

    [Required]
    [Display(Name = "CNPJ")]
    public string CNPJ { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Telefone")]
    public string? Telefone { get; set; }

    [Display(Name = "Responsável")]
    public string? NomeResponsavel { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; } = string.Empty;
}

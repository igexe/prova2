using System.ComponentModel.DataAnnotations;

namespace Models.Entidades;

public class Fornecedor
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Nome Fantasia")]
    public string NomeFantasia { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "Razão Social")]
    public string? RazaoSocial { get; set; }

    [Required, StringLength(20)]
    [Display(Name = "CNPJ")]
    public string CNPJ { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100)]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    [Display(Name = "Telefone")]
    public string? Telefone { get; set; }

    [StringLength(100)]
    [Display(Name = "Nome do Responsável")]
    public string? NomeResponsavel { get; set; }

    [Required, StringLength(255)]
    [Display(Name = "Senha")]
    public string SenhaHash { get; set; } = string.Empty;

    public ICollection<CotacaoParticipante> CotacoesParticipantes { get; set; } = new List<CotacaoParticipante>();
    public ICollection<CotacaoPreco> Precos { get; set; } = new List<CotacaoPreco>();
}
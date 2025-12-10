using System.ComponentModel.DataAnnotations;

namespace Models.Entidades;

public class Produto
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }

    [StringLength(20)]
    public string? UnidadeMedida { get; set; }

    public ICollection<CotacaoItem> ItensCotacao { get; set; } = new List<CotacaoItem>();
}

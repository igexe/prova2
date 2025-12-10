using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel;

public class ProdutoViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nome do Produto")]
    public string Nome { get; set; } = string.Empty;

    [Display(Name = "Descrição")]
    public string? Descricao { get; set; }

    [Display(Name = "Unidade de Medida")]
    public string? UnidadeMedida { get; set; }
}

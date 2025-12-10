using System.ComponentModel.DataAnnotations;

namespace Models.ViewModel;

public class CotacaoItemLinhaViewModel
{
    [Required]
    [Display(Name = "Produto")]
    public int ProdutoId { get; set; }

    [Required]
    [Display(Name = "Quantidade")]
    public int Quantidade { get; set; }

    [Display(Name = "Observação")]
    public string? Observacao { get; set; }
}

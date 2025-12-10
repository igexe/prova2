using System.ComponentModel.DataAnnotations;

namespace Models.Entidades;

public class CotacaoPreco
{
    public int Id { get; set; }

    [Required]
    public int CotacaoItemId { get; set; }

    [Required]
    public int FornecedorId { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal PrecoUnitario { get; set; }

    public DateTime DataResposta { get; set; } = DateTime.Now;

    public CotacaoItem CotacaoItem { get; set; } = null!;
    public Fornecedor Fornecedor { get; set; } = null!;
}

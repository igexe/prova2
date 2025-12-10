using System.ComponentModel.DataAnnotations;

namespace Models.Entidades;

public class CotacaoItem
{
    public int Id { get; set; }

    [Required]
    public int CotacaoId { get; set; }

    [Required]
    public int ProdutoId { get; set; }

    [Required]
    public int Quantidade { get; set; }

    [StringLength(200)]
    public string? Observacao { get; set; }

    public Cotacao Cotacao { get; set; } = null!;
    public Produto Produto { get; set; } = null!;

    public ICollection<CotacaoPreco> Precos { get; set; } = new List<CotacaoPreco>();
}

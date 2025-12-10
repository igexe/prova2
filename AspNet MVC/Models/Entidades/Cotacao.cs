using System.ComponentModel.DataAnnotations;

namespace Models.Entidades;

public class Cotacao
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Descricao { get; set; } = string.Empty;

    public DateTime DataAbertura { get; set; } = DateTime.Now;

    [Required]
    public DateTime DataFechamento { get; set; }

    [Required, StringLength(20)]
    public string Status { get; set; } = "Aberta"; // Aberta, Fechada, Processada

    public ICollection<CotacaoItem> Itens { get; set; } = new List<CotacaoItem>();
    public ICollection<CotacaoParticipante> Participantes { get; set; } = new List<CotacaoParticipante>();
}

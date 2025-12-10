namespace Models.Entidades;

public class CotacaoParticipante
{
    public int Id { get; set; }
    public int CotacaoId { get; set; }
    public int FornecedorId { get; set; }

    public Cotacao Cotacao { get; set; } = null!;
    public Fornecedor Fornecedor { get; set; } = null!;
}

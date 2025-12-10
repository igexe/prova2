namespace Models.ViewModel;

public class CotacaoResultadoFornecedorViewModel
{
    public int FornecedorId { get; set; }
    public string FornecedorNome { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
}

public class CotacaoResultadoViewModel
{
    public int CotacaoId { get; set; }
    public string DescricaoCotacao { get; set; } = string.Empty;
    public DateTime DataFechamento { get; set; }

    public List<CotacaoResultadoFornecedorViewModel> Fornecedores { get; set; } = new();
}

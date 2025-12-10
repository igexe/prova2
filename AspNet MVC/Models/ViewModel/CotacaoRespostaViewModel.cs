namespace Models.ViewModel;

public class CotacaoRespostaItemViewModel
{
    public int CotacaoItemId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public string UnidadeMedida { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public decimal? PrecoUnitario { get; set; }
}

public class CotacaoRespostaViewModel
{
    public int CotacaoId { get; set; }
    public string DescricaoCotacao { get; set; } = string.Empty;
    public DateTime DataFechamento { get; set; }

    public List<CotacaoRespostaItemViewModel> Itens { get; set; } = new();
}
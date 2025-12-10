using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModel;

public class CotacaoCreateViewModel
{
    [Required]
    [Display(Name = "Descrição da Cotação")]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Data de Fechamento")]
    [DataType(DataType.DateTime)]
    public DateTime DataFechamento { get; set; }

    // Itens da cotação
    public List<CotacaoItemLinhaViewModel> Itens { get; set; } = new();

    // Fornecedores selecionados
    [Display(Name = "Fornecedores")]
    public List<int> FornecedoresSelecionadosIds { get; set; } = new();

    // Listas para exibir nos selects
    public List<SelectListItem> ProdutosDisponiveis { get; set; } = new();
    public List<SelectListItem> FornecedoresDisponiveis { get; set; } = new();
}

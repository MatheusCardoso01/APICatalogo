namespace APICatalogo.Pagination;

public class ProdutosFiltroPreco : Parameters
{
    public decimal? Preco { get; set; }
    public string? PrecoCriterio { get; set; } // maior, menor ou igual
}

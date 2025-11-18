using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<PagedList<Produto>> GetProdutosAsync(Parameters produtosParams);
    Task<Produto> GetPrimeiroAsync();
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaEspecificaAsync(int id);

    Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams);
}

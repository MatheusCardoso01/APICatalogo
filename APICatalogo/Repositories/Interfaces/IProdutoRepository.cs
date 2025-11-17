using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    PagedList<Produto> GetProdutos(Parameters produtosParams);
    Produto GetPrimeiro();
    IEnumerable<Produto> GetProdutosPorCategoriaEspecifica(int id);
}

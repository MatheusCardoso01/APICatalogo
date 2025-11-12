using APICatalogo.Models;

namespace APICatalogo.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Produto GetPrimeiro();
    IEnumerable<Produto> GetProdutosPorCategoriaEspecifica(int id);
}

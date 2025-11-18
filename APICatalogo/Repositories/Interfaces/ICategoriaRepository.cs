using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Runtime.InteropServices;

namespace APICatalogo.Repositories.Interfaces;

public interface ICategoriaRepository
{
    Task<PagedList<Categoria>> GetCategoriasAsync(Parameters categoriasParams);
    Task<PagedList<Categoria>> GetCategoriaFiltroNomeAsync(CategoriaFiltroNome categoriaFiltroParams);
    Task<IEnumerable<Categoria>> GetCategoriasAsync();
    Task<Categoria> GetCategoriaAsync(int id);
    Task<Categoria> CreateCategoriaAsync(Categoria categoria);
    Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
    Task<Categoria> DeleteCategoriaAsync(int id);
    Task<IEnumerable<Categoria>> GetCategoriasEProdutosAsync();
}

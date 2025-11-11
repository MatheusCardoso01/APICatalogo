using APICatalogo.Models;
using System.Runtime.InteropServices;

namespace APICatalogo.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetCategoriasAsync();
    Task<Categoria> GetCategoriaAsync(int id);
    Task<Categoria> CreateCategoriaAsync(Categoria categoria);
    Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
    Task<Categoria> DeleteCategoriaAsync(int id);
    Task<IEnumerable<Categoria>> GetCategoriasEProdutosAsync();
}

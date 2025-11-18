using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<PagedList<Cliente>> GetClientesAsync(Parameters clientesParams);
}

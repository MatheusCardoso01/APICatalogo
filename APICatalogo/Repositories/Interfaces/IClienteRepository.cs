using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.Interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    PagedList<Cliente> GetClientes(Parameters clientesParams);
}

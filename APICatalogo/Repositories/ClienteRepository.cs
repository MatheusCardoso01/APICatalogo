using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public PagedList<Cliente> GetClientes(Parameters clientesParams)
        {
            var clientesOrdenados = GetAll().OrderBy(c => c.ClienteId).AsQueryable();
            var clientesPaginados = PagedList<Cliente>.ToPagedList(clientesOrdenados, clientesParams.PageNumber, clientesParams.PageSize);

            return clientesPaginados;
        }
    }
}

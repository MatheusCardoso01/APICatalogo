using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

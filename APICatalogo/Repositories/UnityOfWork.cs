using APICatalogo.Context;
using APICatalogo.Repositories.Interfaces;

namespace APICatalogo.Repositories;

public class UnityOfWork : IUnityOfWork
{
    private IProdutoRepository? _produtoRepo;

    private ICategoriaRepository? _categoriaRepo;

    private IClienteRepository? _clienteRepo;

    public AppDbContext _context;

    public UnityOfWork(AppDbContext context)
    {
        _context = context;
    }

    // propriedades
    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context); // ?? verifica se já existe instância, se não houver cria uma nova, se não, aproveita a existente
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
        }
    }

    public IClienteRepository ClienteRepository
    {
        get 
        {
            return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context);
        }
    }

    // método
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose(); // necessário pois o Garbage Collector não gerencia certos recursos como conexões com banco de dados e arquivos
    }
}

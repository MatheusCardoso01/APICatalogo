using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{

    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Produto GetPrimeiro()
    {
        var primeiroProduto = _context.Produtos.FirstOrDefault();

        return primeiroProduto;
    }

    public IEnumerable<Produto> GetProdutosPorCategoriaEspecifica(int id)
    {
        return GetAll().Where(p => p.CategoriaId == id);
    }
}

using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.Interfaces;
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

    public async Task<Produto> GetPrimeiroAsync()
    {
        var primeiroProduto = await _context.Produtos.FirstOrDefaultAsync();

        return primeiroProduto;
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaEspecificaAsync(int id)
    {
        return (await GetAllAsync()).Where(p => p.CategoriaId == id);
    }

    public async Task<PagedList<Produto>> GetProdutosAsync(Parameters produtosParams)
    {
        var produtosOrdenados = (await GetAllAsync()).OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosPaginados = PagedList<Produto>.ToPagedList(produtosOrdenados, produtosParams.PageNumber, produtosParams.PageSize);

        return produtosPaginados;
    }

    public async Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams)
    {
        var produtos = (await GetAllAsync()).AsQueryable();

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
        {
            if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.ProdutoId).AsQueryable();
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.ProdutoId).AsQueryable();
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.ProdutoId).AsQueryable();
            }
        }

        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

        return produtosFiltrados;
    }
}

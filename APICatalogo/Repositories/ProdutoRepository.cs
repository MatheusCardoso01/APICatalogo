using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class ProdutoRepository : IProdutoRepository
{

    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Produto> GetProdutos()
    {
        var produtos = _context.Produtos.ToList();

        return produtos;
    }

    public Produto GetPrimeiro()
    {
        var primeiroProduto = _context.Produtos.FirstOrDefault();

        return primeiroProduto;
    }

    public Produto GetProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        return produto;
    }

    public Produto CreateProduto(Produto produto)
    {
        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return produto;
    }

    public Produto UpdateProduto(Produto produto)
    {
        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return produto;
    }

    public Produto DeleteProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return produto;
    }
}

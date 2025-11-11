using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
    {
        var categorias = await _context.Categorias.ToListAsync();

        return categorias;
    }

    public async Task<Categoria> GetCategoriaAsync(int id)
    {
        return await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
    }

    public async Task<Categoria> CreateCategoriaAsync(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }

    public async Task<Categoria> UpdateCategoriaAsync(Categoria categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return categoria;
    }

    public async Task<Categoria> DeleteCategoriaAsync(int id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriasEProdutosAsync()
    {
        return await _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 20).Take(100).ToListAsync();
    }
}

using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIWGrandAlpha.DAL.Repositories;

public class CellRepository : IBaseRepository<Cell>
{
    private readonly ApplicationDbContext _context;

    public CellRepository(ApplicationDbContext context)
    {
        _context=context;
    }

    public async Task Create(Cell entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Cell entity)
    {
        _context.Cells.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Cell> Update(Cell entity)
    {
        _context.Cells.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<Cell> GetAll()
    {
        return _context.Cells;
    }
}
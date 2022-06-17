using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;

namespace JIWGrandAlpha.DAL.Repositories;

public class ClassRepository : IBaseRepository<Class>
{
    private readonly ApplicationDbContext _context;

    public ClassRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Class entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Class entity)
    {
        _context.Classes.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<Class> Update(Class entity)
    {
        _context.Classes.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<Class> GetAll()
    {
        return _context.Classes;
    }

}
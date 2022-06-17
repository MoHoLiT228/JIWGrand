using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Object = JIWGrandAlpha.Domain.Entity.Object;

namespace JIWGrandAlpha.DAL.Repositories;

public class ObjectRepository : IBaseRepository<Object>
{
    private readonly ApplicationDbContext _context;

    public ObjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Object entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Object entity)
    {
        _context.Objects.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<Object> Update(Object entity)
    {
        _context.Objects.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<Object> GetAll()
    {
        return _context.Objects;
    }
}
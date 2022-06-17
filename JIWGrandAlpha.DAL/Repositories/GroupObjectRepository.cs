using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIWGrandAlpha.DAL.Repositories;

public class GroupobjectRepository : IBaseRepository<GroupObject>
{
    private readonly ApplicationDbContext _context;

    public GroupobjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(GroupObject entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(GroupObject entity)
    {
        _context.GroupObjects.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<GroupObject> Update(GroupObject entity)
    {
        _context.GroupObjects.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<GroupObject> GetAll()
    {
        return _context.GroupObjects;
    }
}
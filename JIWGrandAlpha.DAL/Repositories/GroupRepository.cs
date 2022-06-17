using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIWGrandAlpha.DAL.Repositories;

public class GroupRepository : IBaseRepository<Group>
{
    private readonly ApplicationDbContext _context;

    public GroupRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(Group entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Group entity)
    {
        _context.Groups.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<Group> Update(Group entity)
    {
        _context.Groups.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<Group> GetAll()
    {
        return _context.Groups;
    }
}
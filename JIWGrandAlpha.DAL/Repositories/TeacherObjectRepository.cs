using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIWGrandAlpha.DAL.Repositories;

public class TeacherObjectRepository : IBaseRepository<TeacherObject>
{
    private readonly ApplicationDbContext _context;

    public TeacherObjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Create(TeacherObject entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(TeacherObject entity)
    {
        _context.TeacherObjects.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<TeacherObject> Update(TeacherObject entity)
    {
        _context.TeacherObjects.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<TeacherObject> GetAll()
    {
        return _context.TeacherObjects;
    }
}
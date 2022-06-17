using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIWGrandAlpha.DAL.Repositories
{
    public class TeacherRepository : IBaseRepository<Teacher>
    {
        private readonly ApplicationDbContext _context;

        public TeacherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Teacher entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Teacher entity)
        {
            _context.Teachers.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<Teacher> Update(Teacher entity)
        {
            _context.Teachers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<Teacher> GetAll()
        {
            return _context.Teachers;
        }
    }
}

using JIWGrandAlpha.DAL.Interfaces;
using JIWGrandAlpha.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace JIWGrandAlpha.DAL.Repositories
{
    public class StudentRepository : IBaseRepository<Student>
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Student entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Student entity)
        {
            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<Student> Update(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<Student> GetAll()
        {
            return _context.Students;
        }
    }
}

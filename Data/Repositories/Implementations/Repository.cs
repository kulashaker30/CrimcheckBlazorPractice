using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using OnlineClinic.Data.Entities;
using System.Threading.Tasks;

namespace OnlineClinic.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly OnlineClinicDbContext _context;

        protected readonly DbSet<T> _dbSet;
        
        public Repository(OnlineClinicDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll(bool asNoTracking = true) => asNoTracking? _dbSet.AsNoTracking().AsEnumerable() : _dbSet.AsEnumerable();

        public virtual Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = true) => Task.FromResult<IEnumerable<T>>(asNoTracking? _dbSet.AsNoTracking().AsEnumerable() : _dbSet.AsEnumerable());

        public virtual T GetById(int id) => _dbSet.AsNoTracking().FirstOrDefault(s => s.Id == id);

        public virtual Task<T> GetByIdAsync(int id) => _dbSet.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);

        public virtual int Create(T entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public virtual async Task<int> CreateAsync(T entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public virtual void Update(T entity) 
        {
            var dateCreated = _dbSet.Where(e => e.Id == entity.Id).Select(e => e.DateCreated).FirstOrDefault();
            _dbSet.Attach(entity);

            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            
            entity.DateCreated = dateCreated;
            entity.DateModified = DateTime.UtcNow;
            
            _context.SaveChanges();
            entry.State = EntityState.Detached;
        }

        public virtual async Task UpdateAsync(T entity) 
        {
            var dateCreated = _dbSet.Where(e => e.Id == entity.Id).Select(e => e.DateCreated).FirstOrDefault();
            _dbSet.Attach(entity);

            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            
            entity.DateCreated = dateCreated;
            entity.DateModified = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            entry.State = EntityState.Detached;
    
        }


        public virtual void Delete(int id)
        {
            T entity = _dbSet.SingleOrDefault(s => s.Id == id);
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task DeleteAsync(int id)
        {
            T entity = _dbSet.SingleOrDefault(s => s.Id == id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id) => _dbSet.Find(id) != null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commence.DataAccess.Data;
using E_Commence.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_Commence.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository (ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
            _context.Products.Include(u => u.Category).Include(u=>u.CategoryId);
           // _context.Categories == dbSet;
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
           
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty( includeProperties))
            {
                foreach (var includeProperty in includeProperties
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                    { 
                        query = query.Include(includeProperty);
                    }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

       
    }
}

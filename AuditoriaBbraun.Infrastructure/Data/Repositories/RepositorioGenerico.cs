using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AuditoriaBbraun.Domain.Entities;
using AuditoriaBbraun.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace AuditoriaBbraun.Infrastructure.Data.Repositories
{
    public class RepositorioGenerico<T> : IRepositorio<T> where T : class
    {
        protected readonly ContextApplications _contexto;
        protected readonly DbSet<T> _dbSet;

        public RepositorioGenerico(ContextApplications contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _dbSet = _contexto.Set<T>();
        }
        public virtual async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ObtenerTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ObtenerTodosAsync(Expression<Func<T, bool>> filtro)
        {
            return await _dbSet.Where(filtro).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ObtenerTodosAsync(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<T> query = _dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> ObtenerPrimeroAsync(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<T> ObtenerUltimoAsync(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.LastOrDefaultAsync();
        }

        public virtual async Task<int> ContarAsync(Expression<Func<T, bool>> filtro = null)
        {
            if (filtro != null)
            {
                return await _dbSet.CountAsync(filtro);
            }
            return await _dbSet.CountAsync();
        }

        public virtual async Task<bool> ExisteAsync(Expression<Func<T, bool>> filtro)
        {
            return await _dbSet.AnyAsync(filtro);
        }

        public virtual async Task<bool> ExisteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity != null;
        }

        public virtual async Task AgregarAsync(T entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            await _dbSet.AddAsync(entidad);
        }

        public virtual async Task AgregarRangoAsync(IEnumerable<T> entidades)
        {
            if (entidades == null)
                throw new ArgumentNullException(nameof(entidades));

            await _dbSet.AddRangeAsync(entidades);
        }

        public virtual async Task ActualizarAsync(T entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            _dbSet.Update(entidad);
            await Task.CompletedTask;
        }

        public virtual async Task ActualizarRangoAsync(IEnumerable<T> entidades)
        {
            if (entidades == null)
                throw new ArgumentNullException(nameof(entidades));

            _dbSet.UpdateRange(entidades);
            await Task.CompletedTask;
        }

        public virtual async Task EliminarAsync(T entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            _dbSet.Remove(entidad);
            await Task.CompletedTask;
        }

        public virtual async Task EliminarRangoAsync(IEnumerable<T> entidades)
        {
            if (entidades == null)
                throw new ArgumentNullException(nameof(entidades));

            _dbSet.RemoveRange(entidades);
            await Task.CompletedTask;
        }

        public virtual async Task EliminarAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            await Task.CompletedTask;
        }

        public virtual async Task EliminarAsync(Expression<Func<T, bool>> filtro)
        {
            var entities = await _dbSet.Where(filtro).ToListAsync();
            if (entities.Any())
            {
                _dbSet.RemoveRange(entities);
            }
            await Task.CompletedTask;
        }

        public virtual IQueryable<T> Consulta()
        {
            return _dbSet.AsQueryable();
        }

        public virtual void Detach(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _contexto.Entry(entity).State = EntityState.Detached;
        }

        public virtual async Task<IEnumerable<T>> BuscarAsync(
            Expression<Func<T, bool>> predicado,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicado != null)
            {
                query = query.Where(predicado);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }
    }
}

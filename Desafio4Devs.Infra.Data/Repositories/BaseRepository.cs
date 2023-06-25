using Desafio4Devs.Domain.Interfaces.Repositories;
using Desafio4Devs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Desafio4Devs.Infra.Data.Repositories
{
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext _appContext;

        public BaseRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<T> Add(T entity)
        {
            _appContext.Add(entity);
            await _appContext.SaveChangesAsync();

            return entity;
        }

        public void Dispose()
        {
            _appContext.DisposeAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _appContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _appContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _appContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetListByPredicate(Expression<Func<T, bool>> predicate)
        {
            return await _appContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> Remove(T entity)
        {
            return await this.Update(entity);
        }

        public async Task<T> Update(T entity)
        {
            _appContext.Entry(entity).State = EntityState.Modified;
            await _appContext.SaveChangesAsync();

            return entity;
        }
    }
}
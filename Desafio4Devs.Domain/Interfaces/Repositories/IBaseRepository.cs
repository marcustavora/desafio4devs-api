using System.Linq.Expressions;

namespace Desafio4Devs.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Add(T entity);

        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetListByPredicate(Expression<Func<T, bool>> predicate);

        Task<T> Update(T entity);

        Task<T> Remove(T entity);

        void Dispose();
    }
}
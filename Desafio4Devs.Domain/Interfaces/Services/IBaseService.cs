namespace Desafio4Devs.Domain.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<T> Add(T entity);

        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Update(T entity);

        Task<T> Remove(T entity);

        void Dispose();
    }
}
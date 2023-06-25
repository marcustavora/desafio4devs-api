using Desafio4Devs.Domain.Interfaces.Repositories;
using Desafio4Devs.Domain.Interfaces.Services;

namespace Desafio4Devs.Domain.Services
{
    public class BaseService<T> : IDisposable, IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<T> Add(T entity)
        {
            return await _baseRepository.Add(entity);
        }

        public void Dispose()
        {
            _baseRepository.Dispose();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<T> Remove(T entity)
        {
            return await _baseRepository.Remove(entity);
        }

        public async Task<T> Update(T entity)
        {
            return await _baseRepository.Update(entity);
        }
    }
}
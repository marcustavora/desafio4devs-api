using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> ObterTodosClientes();

        Task<IEnumerable<Cliente>> ObterClientesPorNome(string nome);
    }
}
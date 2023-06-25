using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Desafio4Devs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio4Devs.Infra.Data.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        private readonly ApplicationContext _appContext;

        public ClienteRepository(ApplicationContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<Cliente>> ObterTodosClientes()
        {
            return await _appContext.Cliente.Include(c => c.ClienteAvaliacao)
                                            .ThenInclude(c => c.Avaliacao)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterClientesPorNome(string nome)
        {
            return await _appContext.Cliente.Include(c => c.ClienteAvaliacao)
                                            .ThenInclude(c => c.Avaliacao)
                                            .Where(c => c.Nome.Contains(nome))
                                            .ToListAsync();
        }
    }
}
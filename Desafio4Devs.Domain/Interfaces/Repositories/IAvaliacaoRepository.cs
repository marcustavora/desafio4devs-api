using Desafio4Devs.Domain.Dto.Avaliacoes;
using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Interfaces.Repositories
{
    public interface IAvaliacaoRepository : IBaseRepository<Avaliacao>
    {
        Task<IEnumerable<Avaliacao>> ObterPorFiltro(AvaliacaoFiltroDto filtro);
    }
}
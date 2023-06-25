using Desafio4Devs.Domain.Dto.Avaliacoes;
using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Desafio4Devs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Desafio4Devs.Infra.Data.Repositories
{
    public class AvaliacaoRepository : BaseRepository<Avaliacao>, IAvaliacaoRepository
    {
        private readonly ApplicationContext _appContext;

        public AvaliacaoRepository(ApplicationContext appContext) : base(appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<Avaliacao>> ObterPorFiltro(AvaliacaoFiltroDto filtro)
        {
            IQueryable<Avaliacao> query = _appContext.Avaliacao.Include(c => c.ClienteAvaliacao);

            if (!string.IsNullOrEmpty(filtro?.MesAnoReferencia))
                query = query.Where(a => a.MesAnoReferencia == filtro.MesAnoReferencia);

            if (filtro != null && filtro.AvaliacaoId.HasValue)
                query = query.Where(a => a.Id == filtro.AvaliacaoId);

            return await query.OrderByDescending(a => a.MesAnoReferencia).ToListAsync();
        }

        public new async Task<Avaliacao> GetById(int id)
        {
            return await _appContext.Avaliacao.Include(c => c.ClienteAvaliacao).FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
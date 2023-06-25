using Desafio4Devs.Domain.Dto.Avaliacoes;
using Desafio4Devs.Domain.Dto.Infra;
using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Interfaces.Services
{
    public interface IAvaliacaoService : IBaseService<Avaliacao>
    {
        Task<ResponseDto<bool>> CriarAvaliacao(Avaliacao model);

        Task<ResponseDto<IEnumerable<AvaliacaoListaDto>>> ObterPorFiltro(AvaliacaoFiltroDto filtro);

        Task<ResponseDto<AvaliacaoDto>> ObterAvaliacaoPorId(int id);

        Task<ResponseDto<RelatorioResultadoDto>> ObterRelatorioResultado(AvaliacaoFiltroDto filtro);
    }
}
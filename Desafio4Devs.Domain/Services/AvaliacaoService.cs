using Desafio4Devs.Domain.Dto.Avaliacoes;
using Desafio4Devs.Domain.Dto.Infra;
using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Enums;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Desafio4Devs.Domain.Interfaces.Services;

namespace Desafio4Devs.Domain.Services
{
    public class AvaliacaoService : BaseService<Avaliacao>, IAvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository) : base(avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<ResponseDto<bool>> CriarAvaliacao(Avaliacao model)
        {
            try
            {
                var avaliacaoExistente = await _avaliacaoRepository.GetByPredicate(x => x.MesAnoReferencia == model.MesAnoReferencia);

                if (avaliacaoExistente != null)
                    return new ResponseDto<bool>(StatusResponse.Erro, "Já existe uma avaliação para o Mês/Ano informado!", false);

                await _avaliacaoRepository.Add(model);
                return new ResponseDto<bool>(StatusResponse.Sucesso, "Avaliação criada com sucesso!", true);
            }
            catch
            {
                return new ResponseDto<bool>(StatusResponse.Erro, "Erro ao criar avaliação!", false);
            }
        }

        public async Task<ResponseDto<IEnumerable<AvaliacaoListaDto>>> ObterPorFiltro(AvaliacaoFiltroDto filtro)
        {
            try
            {
                var model = await _avaliacaoRepository.ObterPorFiltro(filtro);
                var avaliacao = model.Select(a => new AvaliacaoListaDto(a));
                return new ResponseDto<IEnumerable<AvaliacaoListaDto>>(StatusResponse.Sucesso, string.Empty, avaliacao);
            }
            catch
            {
                return new ResponseDto<IEnumerable<AvaliacaoListaDto>>(StatusResponse.Erro, "Erro ao obter avaliações!", null);
            }
        }

        public async Task<ResponseDto<AvaliacaoDto>> ObterAvaliacaoPorId(int id)
        {
            try
            {
                var model = await _avaliacaoRepository.GetById(id);
                var avaliacao = model.ToDto();
                return new ResponseDto<AvaliacaoDto>(StatusResponse.Sucesso, string.Empty, avaliacao);
            }
            catch
            {
                return new ResponseDto<AvaliacaoDto>(StatusResponse.Erro, "Erro ao obter avaliação por id!", null);
            }
        }

        public async Task<ResponseDto<RelatorioResultadoDto>> ObterRelatorioResultado(AvaliacaoFiltroDto filtro)
        {
            try
            {
                var model = await _avaliacaoRepository.ObterPorFiltro(filtro);

                var relatorio = new RelatorioResultadoDto(model.ToList());

                return new ResponseDto<RelatorioResultadoDto>(StatusResponse.Sucesso, string.Empty, relatorio);
            }
            catch
            {
                return new ResponseDto<RelatorioResultadoDto>(StatusResponse.Erro, "Erro ao obter relatório resultado!", null);
            }
        }
    }
}
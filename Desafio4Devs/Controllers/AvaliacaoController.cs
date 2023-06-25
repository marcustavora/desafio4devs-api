using Desafio4Devs.Domain.Dto.Avaliacoes;
using Desafio4Devs.Domain.Enums;
using Desafio4Devs.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio4Devs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : Controller
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AvaliacaoDto avaliacao)
        {
            var model = avaliacao.ToEntity();

            var result = await _avaliacaoService.CriarAvaliacao(model);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _avaliacaoService.ObterAvaliacaoPorId(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("ObterPorFiltro")]
        public async Task<IActionResult> ObterPorFiltro([FromQuery]AvaliacaoFiltroDto filtro)
        {
            var result = await _avaliacaoService.ObterPorFiltro(filtro);
            return Ok(result);
        }

        [HttpGet]
        [Route("ObterRelatorioResultado")]
        public async Task<IActionResult> ObterRelatorioResultado([FromQuery] AvaliacaoFiltroDto filtro)
        {
            var result = await _avaliacaoService.ObterRelatorioResultado(filtro);
            return Ok(result);
        }
    }
}
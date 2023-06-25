using Desafio4Devs.Domain.Dto.Clientes;
using Desafio4Devs.Domain.Enums;
using Desafio4Devs.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio4Devs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        //[Authorize(Roles = UsuarioNivelDto.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto cliente)
        {
            var model = cliente.ToEntity();

            var result = await _clienteService.CriarCliente(model);

            return Ok(result);
        }

        //[Authorize(Roles = UsuarioNivelDto.Admin)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClienteDto cliente)
        {
            var model = cliente.ToEntity();
            model.Id = id;

            var result = await _clienteService.AlterarCliente(model);

            return Ok(result);
        }

        //[Authorize(Roles = UsuarioNivelDto.Admin)]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _clienteService.ExcluirCliente(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterPorId([FromRoute] int id)
        {
            var result = await _clienteService.ObterClientePorId(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("ObterPorNome")]
        public async Task<IActionResult> ObterPorNome([FromQuery] string? nome)
        {
            var result = await _clienteService.ObterClientesPorNome(nome);
            return Ok(result);
        }
    }
}
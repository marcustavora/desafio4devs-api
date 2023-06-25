using Desafio4Devs.Domain.Dto.Infra;
using Desafio4Devs.Domain.Dto.Usuarios;
using Desafio4Devs.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio4Devs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : Controller
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var token = await _autenticacaoService.Autenticar(login.Username, login.Senha);

            if (token != null)
                return Ok(token);

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("CriarUsuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] UsuarioDto usuario)
        {
            var result = await _autenticacaoService.CriarUsuario(usuario);

            return Ok(result);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenDto token)
        {
            if (token is null)
                return BadRequest();

            var novoToken = await _autenticacaoService.AtualizarToken(token);

            if (novoToken != null)
                return Ok(novoToken);

            return BadRequest();
        }

        [HttpPost, Authorize]
        [Route("Revoke")]
        public async Task<IActionResult> Revoke()
        {
            var result = await _autenticacaoService.RevogarToken(User.Identity.Name);

            if (result)
                return NoContent();
            else
                return BadRequest();
        }
    }
}

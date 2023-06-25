using Desafio4Devs.Domain.Dto.Infra;
using Desafio4Devs.Domain.Dto.Usuarios;

namespace Desafio4Devs.Domain.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        Task<AuthResponseDto> Autenticar(string username, string senha);

        Task<ResponseDto<bool>> CriarUsuario(UsuarioDto usuario);

        Task<AuthResponseDto> AtualizarToken(TokenDto token);

        Task<bool> RevogarToken(string username);
    }
}
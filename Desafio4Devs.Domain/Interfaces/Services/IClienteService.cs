using Desafio4Devs.Domain.Dto.Clientes;
using Desafio4Devs.Domain.Dto.Infra;
using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Interfaces.Services
{
    public interface IClienteService : IBaseService<Cliente>
    {
        Task<ResponseDto<bool>> CriarCliente(Cliente model);

        Task<ResponseDto<bool>> AlterarCliente(Cliente model);

        Task<ResponseDto<bool>> ExcluirCliente(int id);

        Task<ResponseDto<ClienteDto>> ObterClientePorId(int id);

        Task<ResponseDto<IEnumerable<ClienteListaDto>>> ObterClientesPorNome(string nome);
    }
}
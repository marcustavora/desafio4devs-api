using Desafio4Devs.Domain.Dto.Clientes;
using Desafio4Devs.Domain.Dto.Infra;
using Desafio4Devs.Domain.Entities;
using Desafio4Devs.Domain.Enums;
using Desafio4Devs.Domain.Interfaces.Repositories;
using Desafio4Devs.Domain.Interfaces.Services;

namespace Desafio4Devs.Domain.Services
{
    public class ClienteService : BaseService<Cliente>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository) : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseDto<bool>> CriarCliente(Cliente model)
        {
            try
            {
                var clienteExistente = await _clienteRepository.GetByPredicate(x => x.Cnpj == model.Cnpj);

                if (clienteExistente != null)
                    return new ResponseDto<bool>(StatusResponse.Erro, "Já existe um cliente com o CNPJ informado!", false);

                await _clienteRepository.Add(model);
                return new ResponseDto<bool>(StatusResponse.Sucesso, "Cliente criado com sucesso!", true);
            }
            catch
            {
                return new ResponseDto<bool>(StatusResponse.Erro, "Erro ao criar cliente!", false);
            }
        }

        public async Task<ResponseDto<bool>> AlterarCliente(Cliente model)
        {
            try
            {
                var clienteExistente = await _clienteRepository.GetByPredicate(x => x.Cnpj == model.Cnpj && x.Id != model.Id);

                if (clienteExistente != null)
                    return new ResponseDto<bool>(StatusResponse.Erro, "Já existe um cliente com o CNPJ informado!", false);

                var clienteDb = await _clienteRepository.GetById(model.Id);
                clienteDb.Nome = model.Nome;
                clienteDb.NomeContato = model.NomeContato;
                clienteDb.Cnpj = model.Cnpj;
                clienteDb.DataCliente = model.DataCliente;

                await _clienteRepository.Update(clienteDb);
                return new ResponseDto<bool>(StatusResponse.Sucesso, "Cliente alterado com sucesso!", true);
            }
            catch
            {
                return new ResponseDto<bool>(StatusResponse.Erro, "Erro ao alterar cliente!", false);
            }
        }

        public async Task<ResponseDto<bool>> ExcluirCliente(int id)
        {
            try
            {
                var clienteDb = await _clienteRepository.GetById(id);

                if (clienteDb == null)
                    return new ResponseDto<bool>(StatusResponse.Erro, "Cliente não existe!", false);

                clienteDb.Ativo = false;

                await _clienteRepository.Update(clienteDb);
                return new ResponseDto<bool>(StatusResponse.Sucesso, "Cliente excluído com sucesso!", true);
            }
            catch
            {
                return new ResponseDto<bool>(StatusResponse.Erro, "Erro ao excluir cliente!", false);
            }
        }

        public async Task<ResponseDto<ClienteDto>> ObterClientePorId(int id)
        {
            try
            {
                var model = await _clienteRepository.GetById(id);
                var cliente = model.ToDto();
                return new ResponseDto<ClienteDto>(StatusResponse.Sucesso, string.Empty, cliente);
            }
            catch
            {
                return new ResponseDto<ClienteDto>(StatusResponse.Erro, "Erro ao obter cliente por id!", null);
            }
        }

        public async Task<ResponseDto<IEnumerable<ClienteListaDto>>> ObterClientesPorNome(string nome)
        {
            try
            {
                IEnumerable<Cliente> model;

                if (nome == null)
                    model = await _clienteRepository.ObterTodosClientes();
                else
                    model = await _clienteRepository.ObterClientesPorNome(nome);

                var clientes = model.Select(c => new ClienteListaDto(c));

                return new ResponseDto<IEnumerable<ClienteListaDto>>(StatusResponse.Sucesso, string.Empty, clientes);
            }
            catch
            {
                return new ResponseDto<IEnumerable<ClienteListaDto>>(StatusResponse.Erro, "Erro ao obter cliente por nome!", null);
            }
        }
    }
}
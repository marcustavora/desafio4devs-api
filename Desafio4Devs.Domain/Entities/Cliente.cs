using Desafio4Devs.Domain.Dto.Clientes;

namespace Desafio4Devs.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public Cliente()
        {
            Ativo = true;
        }

        public string Nome { get; set; }

        public string NomeContato { get; set; }

        public string Cnpj { get; set; }

        public DateTime DataCliente { get; set; }

        public bool Ativo { get; set; }

        public IEnumerable<ClienteAvaliacao> ClienteAvaliacao { get; set; }

        public ClienteDto ToDto()
        {
            return new ClienteDto
            {
                Nome = Nome,
                NomeContato = NomeContato,
                Cnpj = Cnpj,
                DataCliente = DataCliente
            };
        }
    }
}
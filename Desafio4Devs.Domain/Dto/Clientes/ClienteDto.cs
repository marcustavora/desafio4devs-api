using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Dto.Clientes
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string NomeContato { get; set; }

        public string? Cnpj { get; set; }

        public DateTime DataCliente { get; set; }

        public Cliente ToEntity()
        {
            return new Cliente
            {
                Id = Id,
                Nome = Nome,
                NomeContato = NomeContato,
                Cnpj = Cnpj,
                DataCliente = DataCliente
            };
        }
    }
}
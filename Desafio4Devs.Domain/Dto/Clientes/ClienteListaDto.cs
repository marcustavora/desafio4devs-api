using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Dto.Clientes
{
    public class ClienteListaDto
    {
        public ClienteListaDto(Cliente model)
        {
            Id = model.Id;
            Nome = model.Nome;
            NomeContato = model.NomeContato;
            Cnpj = model.Cnpj;

            var avaliacoes = model.ClienteAvaliacao?.Select(c => new ClienteAvaliacaoListaDto(c));

            if (avaliacoes != null)
            {
                var ultimaAvaliacao = avaliacoes.OrderByDescending(a => a.DataAvaliacao).FirstOrDefault();

                MesAnoReferencia = ultimaAvaliacao?.MesAnoReferencia;
                DataUltimaAvaliacao = ultimaAvaliacao?.DataAvaliacao;
                Nota = ultimaAvaliacao?.Nota;
                MotivoNota = ultimaAvaliacao?.MotivoNota;
            }
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public string NomeContato { get; set; }

        public string Cnpj { get; set; }

        public string? MesAnoReferencia { get; set; }

        public DateTime? DataUltimaAvaliacao { get; set; }

        public int? Nota { get; set; }

        public string? MotivoNota { get; set; }
    }
}
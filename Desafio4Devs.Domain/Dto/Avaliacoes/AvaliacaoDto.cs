using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Dto.Avaliacoes
{
    public class AvaliacaoDto
    { 
        public string MesAnoReferencia { get; set; }

        public DateTime DataAvaliacao { get; set; }

        public ICollection<ClienteAvaliacaoDto> ClienteAvaliacao { get; set; }

        public Avaliacao ToEntity()
        {
            var clienteAvaliacao = ClienteAvaliacao.Select(c => new ClienteAvaliacao(c.ClienteId, c.Nota, c.MotivoNota)).ToList();


            return new Avaliacao(MesAnoReferencia, clienteAvaliacao);
        }
    }
}
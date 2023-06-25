using Desafio4Devs.Domain.Dto.Avaliacoes;

namespace Desafio4Devs.Domain.Entities
{
    public class Avaliacao : EntityBase
    {
        public Avaliacao()
        {

        }

        public Avaliacao(string mesAnoReferencia, ICollection<ClienteAvaliacao> clienteAvaliacao)
        {
            DataAvaliacao = DateTime.Now;
            MesAnoReferencia = mesAnoReferencia;
            ClienteAvaliacao = clienteAvaliacao;
        }

        public DateTime DataAvaliacao { get; set; }

        public string MesAnoReferencia { get; set; }

        public ICollection<ClienteAvaliacao> ClienteAvaliacao { get; set; }

        public AvaliacaoDto ToDto()
        {
            return new AvaliacaoDto
            {
                MesAnoReferencia = MesAnoReferencia,
                DataAvaliacao = DataAvaliacao,
                ClienteAvaliacao = ClienteAvaliacao.Select(c => new ClienteAvaliacaoDto(c.ClienteId, c.Nota, c.MotivoNota)).ToList()
            };
        }
    }
}
using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Dto.Avaliacoes
{
    public class AvaliacaoListaDto
    {
        public AvaliacaoListaDto(Avaliacao model)
        {
            Id = model.Id;
            DataAvaliacao = model.DataAvaliacao;
            MesAnoReferencia = model.MesAnoReferencia;
            QtdClientesAvaliados = model.ClienteAvaliacao.Count();
        }

        public int Id { get; set; }

        public DateTime DataAvaliacao { get; set; }

        public string MesAnoReferencia { get; set; }

        public int QtdClientesAvaliados { get; set; }
    }
}
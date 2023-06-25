using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Dto.Clientes
{
    public class ClienteAvaliacaoListaDto
    {
        public ClienteAvaliacaoListaDto(ClienteAvaliacao model)
        {
            DataAvaliacao = model.Avaliacao.DataAvaliacao;
            MesAnoReferencia = model.Avaliacao.MesAnoReferencia;
            DataAvaliacao = model.Avaliacao.DataAvaliacao;
            Nota = model.Nota;
            MotivoNota = model.MotivoNota;
        }

        public DateTime DataAvaliacao { get; set; }

        public string MesAnoReferencia { get; set; }

        public int Nota { get; set; }

        public string MotivoNota { get; set; }
    }
}
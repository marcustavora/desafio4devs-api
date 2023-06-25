namespace Desafio4Devs.Domain.Entities
{
    public class ClienteAvaliacao : EntityBase
    {
        public ClienteAvaliacao(int clienteId, int nota, string motivoNota)
        {
            ClienteId = clienteId;
            Nota = nota;
            MotivoNota = motivoNota;
        }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int AvaliacaoId { get; set; }
        public virtual Avaliacao Avaliacao { get; set; }

        public int Nota { get; set; }

        public string MotivoNota { get; set; }
    }
}
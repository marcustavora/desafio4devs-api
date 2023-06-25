namespace Desafio4Devs.Domain.Dto.Avaliacoes
{
    public  class ClienteAvaliacaoDto
    {
        public ClienteAvaliacaoDto()
        {

        }

        public ClienteAvaliacaoDto(int clienteId, int nota, string motivaNota)
        {
            ClienteId = clienteId;
            Nota = nota;
            MotivoNota = motivaNota;
        }

        public int ClienteId { get; set; }

        public int Nota { get; set; }

        public string MotivoNota { get; set; }
    }
}
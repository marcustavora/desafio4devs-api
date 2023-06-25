using Desafio4Devs.Domain.Enums;

namespace Desafio4Devs.Domain.Dto.Infra
{
    public class ResponseDto<T>
    {
        public ResponseDto(StatusResponse status, string mensagem, T dados)
        {
            Status = status;
            Mensagem = mensagem;
            Dados = dados;
        }

        public StatusResponse Status { get; set; }

        public string Mensagem { get; set; }

        public T Dados { get; set; }
    }
}
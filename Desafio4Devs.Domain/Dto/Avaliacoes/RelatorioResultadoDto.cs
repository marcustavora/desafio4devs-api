using Desafio4Devs.Domain.Entities;

namespace Desafio4Devs.Domain.Dto.Avaliacoes
{
    public class RelatorioResultadoDto
    {
        public RelatorioResultadoDto(List<Avaliacao> avaliacoes)
        {
            MesAnoReferencia = new List<string>();
            Nps = new List<ValorNpsDto>();

            foreach (var ava in avaliacoes.OrderBy(a => a.MesAnoReferencia).ToList())
            {
                MesAnoReferencia.Add($"{ava.MesAnoReferencia.Substring(0,2)}/{ava.MesAnoReferencia.Substring(2, 4)}");

                var quantidadeClientesAvaliacao = ava.ClienteAvaliacao.Count();
                var quantidadePromotores = ava.ClienteAvaliacao.Where(c => c.Nota >= 9).Count();
                var quantidadeDetratores = ava.ClienteAvaliacao.Where(c => c.Nota <= 6).Count();
                var nps = Math.Round((((double)quantidadePromotores - quantidadeDetratores) / quantidadeClientesAvaliacao) * 100.0, 2);

                Nps.Add(new ValorNpsDto(ava.Id, nps, Cor(nps)));
            }
        }

        public List<string> MesAnoReferencia { get; set; }

        public List<ValorNpsDto> Nps { get; set; }

        public string Cor(double valor)
        {
            if (valor >= 80.0)
                return "#3dc348";

            if (valor >= 60.0)
                return "#fed700";

            return "#e4011c";
        }
    }

    public class ValorNpsDto
    {
        public ValorNpsDto(int avaliacaoId, double valor, string cor)
        {
            AvaliacaoId = avaliacaoId;
            Value = valor;
            ItemStyle = new EstiloBarraDto(cor);
        }

        public int AvaliacaoId { get; set; }

        public double Value { get; set; }

        public EstiloBarraDto ItemStyle { get; set; }
    }

    public class EstiloBarraDto
    {
        public EstiloBarraDto(string cor)
        {
            Color = cor;
            Opacity = 0.5;
        }

        public string Color { get; set; }

        public double Opacity { get; set; }
    }
}
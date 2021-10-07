namespace DotNet.Ranking.Bacen
{
    public class BancosEFinanceiras
    {
        public string Posicao { get; set; }
        public string InstituicaoFinanceira { get; set; }
        public string Indice { get; set; }
        public string ReclamacoesReguladasProcedentes { get; set; }
        public string ReclamacoesReguladasOutras { get; set; }
        public string ReclamacoesNaoReguladas { get; set; }
        public string TotalReclamacoes { get; set; }
        public string QuantidadeClientes { get; set; }
        public string[] Participantes { get; set; }
    }
}
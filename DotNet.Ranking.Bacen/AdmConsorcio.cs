namespace DotNet.Ranking.Bacen
{
    public class AdmConsorcio
    {
        public string Posicao { get; set; }
        public string NomeAdmConsorcio { get; set; }
        public string Indice { get; set; }
        public int ReclamacoesReguladasProcedentes { get; set; }
        public int ReclamacoesReguladasOutras { get; set; }
        public int ReclamacoesNaoReguladas { get; set; }
        public int TotalReclamacoes { get; set; }
    }
}

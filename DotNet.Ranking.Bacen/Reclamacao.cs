using System.Collections.Generic;

namespace DotNet.Ranking.Bacen
{
    public class Reclamacao
    {
        public string Posicao { get; set; }
        public string Motivo { get; set; }
        public int Quantidade { get; set; }
        public List<BancosEFinanceiras> Instituicoes { get; set; }
    }
}

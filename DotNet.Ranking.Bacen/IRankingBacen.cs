using System.Threading.Tasks;

namespace DotNet.Ranking.Bacen
{
    /// <summary>
    /// Funções que buscam informaçãoes do BACEN.
    /// </summary>
    public interface IRankingBacen
    {
        Task<Top3BF> GetTop03BancosEFinanceirasAsync();
        Task<Top3BF> GetTop03DemaisBancosEFinanceirasAsync();
        Task<Top3Reclamacoes> GetTop03ReclamacoesAsync();
        Task<Top03AdmConsorcio> GetTop03AdmConsorcioAsync();
        Top10BF GetTop10BancosEFinanceiras();
        TodasReclamacoes GetTodasReclamacoes();
        TodasAdmConsorcio GetTodasAdmsConsorcio();

    }
}
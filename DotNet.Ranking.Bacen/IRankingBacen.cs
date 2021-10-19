using System.Threading.Tasks;

namespace DotNet.Ranking.Bacen
{
    /// <summary>
    /// Fun��es que buscam informa��oes do BACEN.
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
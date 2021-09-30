using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace DotNet.Ranking.Bacen
{
    public class RankingBacen : IRankingBacen
    {

        private readonly IHttpClientWrapper _httpClient;

        public RankingBacen(IHttpClientWrapper clientFactory)
        {

            _httpClient = clientFactory;
        }

        public async Task<Top3BF> GetTop03BancosEFinanceirasAsync()
        {
            try
            {
                var result = await GetTop03BancosEFinanceiras().ConfigureAwait(false);

                return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch(HtmlWebException)
            {
                throw;
            }
           

        }

        public async Task<Top3BF> GetTop03DemaisBancosEFinanceirasAsync()
        {
            try
            {
                var result = await GetTop03DemaisBancosEFinanceiras().ConfigureAwait(false);

                return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (HtmlWebException)
            {
                throw;
            }
        }

        public async Task<Top3Reclamacoes> GetTop03ReclamacoesAsync()
        {
            try
            {
                var result = await GetTop03Reclamacoes().ConfigureAwait(false);

                return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (HtmlWebException)
            {
                throw;
            }
        }

        public async Task<Top03AdmConsorcio> GetTop03AdmConsorcioAsync()
        {
            try
            {
                var result = await GetTop03AdmConsorcio().ConfigureAwait(false);

                return result;
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (HtmlWebException)
            {
                throw;
            }
        }

        #region Private Methods
        private async Task<Top3BF> GetTop03BancosEFinanceiras()
        {

            var result = await _httpClient.GetAsync().ConfigureAwait(false);

            var response = result;
            var xPath = Constants.XPATH_TABLE_BF;

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var document = new HtmlDocument();
                document.LoadHtml(html);
                var coll = document.DocumentNode.SelectNodes(xPath);

                var top3 = BuildTop3BancosEFinanceiras(coll);

                return top3;
                
            }

            return new Top3BF();

        }

        private async Task<Top3BF> GetTop03DemaisBancosEFinanceiras()
        {

            var result = await _httpClient.GetAsync().ConfigureAwait(false);

            var response = result;
            var xPath = Constants.XPATH_TABLE_DBF;

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var document = new HtmlDocument();
                document.LoadHtml(html);
                var coll = document.DocumentNode.SelectNodes(xPath);

                var top3 = BuildTop3DemaisBancosEFinanceiras(coll);

                return top3;

            }

            return new Top3BF();

        }

        private async Task<Top3Reclamacoes> GetTop03Reclamacoes()
        {

            var result = await _httpClient.GetAsync().ConfigureAwait(false);

            var response = result;
            var xPath = Constants.XPATH_TABLE_REC;

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var document = new HtmlDocument();
                document.LoadHtml(html);
                var coll = document.DocumentNode.SelectNodes(xPath);

                var top3 = BuildTop3Reclamacoes(coll);

                return top3;

            }

            return new Top3Reclamacoes();

        }

        private async Task<Top03AdmConsorcio> GetTop03AdmConsorcio()
        {

            var result = await _httpClient.GetAsync().ConfigureAwait(false);

            var response = result;
            var xPath = Constants.XPATH_TABLE_ADM;

            if (response.IsSuccessStatusCode)
            {
                var html = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var document = new HtmlDocument();
                document.LoadHtml(html);
                var coll = document.DocumentNode.SelectNodes(xPath);

                var top3 = BuildTop3AdmConsorcio(coll);

                return top3;

            }

            return new Top03AdmConsorcio();

        }

        private static Top3BF BuildTop3BancosEFinanceiras(HtmlNodeCollection collection)
        {
            var top3 = new Top3BF() { BancosFinanceiras = new BancosEFinanceiras[Constants.AMOUNT_OBJECTS] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS; i++)
            {
                var position = collection[0].ChildNodes[5].ChildNodes[i+1].ChildNodes[1].ChildNodes[1];
                var institutionName = collection[0].ChildNodes[5].ChildNodes[i+1].ChildNodes[1].ChildNodes[3];
                var index = collection[0].ChildNodes[5].ChildNodes[i+1].ChildNodes[1].ChildNodes[5];
                
                top3.BancosFinanceiras[i] = new BancosEFinanceiras
                {
                    Posicao = position.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    InstituicaoFinanceira = institutionName.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Indice = index.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty)
                    
                };
            }

            return top3;
        }

        private static Top3BF BuildTop3DemaisBancosEFinanceiras(HtmlNodeCollection collection)
        {
            var top3 = new Top3BF() { BancosFinanceiras = new BancosEFinanceiras[Constants.AMOUNT_OBJECTS] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS; i++)
            {
                var position = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[1];
                var institutionName = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[3];
                var index = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[5];
                
                top3.BancosFinanceiras[i] = new BancosEFinanceiras
                {
                    Posicao = position.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    InstituicaoFinanceira = institutionName.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Indice = index.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty)
                };
            }

            return top3;
        }

        private static Top3Reclamacoes BuildTop3Reclamacoes(HtmlNodeCollection collection)
        {
            var top3 = new Top3Reclamacoes() { Reclamacoes = new Reclamacao[Constants.AMOUNT_OBJECTS] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS; i++)
            {
                var position = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[1];
                var reason = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[3];

                top3.Reclamacoes[i] = new Reclamacao
                {
                    Posicao = position.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    Motivo = reason.InnerText.Trim()
                };
            }

            return top3;
        }

        private static Top03AdmConsorcio BuildTop3AdmConsorcio(HtmlNodeCollection collection)
        {
            var top3 = new Top03AdmConsorcio() { AdministradorasConsorcio = new AdmConsorcio[Constants.AMOUNT_OBJECTS] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS; i++)
            {
                var position = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[1];
                var institutionName = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[3];
                var index = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[5];

                top3.AdministradorasConsorcio[i] = new AdmConsorcio
                {
                    Posicao = position.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    Indice = index.InnerText.Trim(),
                    NomeAdmConsorcio = institutionName.InnerText.Trim()
                };
            }

            return top3;
        }

        #endregion

    }
}

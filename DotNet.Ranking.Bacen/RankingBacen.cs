using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotNet.Ranking.Bacen
{
    public class RankingBacen : IRankingBacen
    {

        private readonly IHttpClientWrapper _httpClient;
        private readonly HtmlDocument _document;
        private readonly ChromeDriver _driver;

        public RankingBacen(IHttpClientWrapper clientFactory)
        {

            _httpClient = clientFactory;
        }

        public RankingBacen(ChromeOptions driverOptions, string chromeDriverPath)
        {
            if (string.IsNullOrEmpty(chromeDriverPath)) 
            {
                throw new ArgumentException("chromeDriverPath cannot be null");
            }

            _driver = new ChromeDriver(chromeDriverPath, driverOptions);
            _document = new HtmlDocument();
            
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

        public Top10BF GetTop10BancosEFinanceiras()
        {
            try
            {
                var result = GetTop10();

                return result;
            }
            catch (HtmlWebException)
            {
                throw;
            }
            catch (NoSuchElementException)
            {
                throw;
            }
            catch (WebDriverException)
            {
                throw;
            }
            catch (NodeNotFoundException)
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

        private Top10BF GetTop10()
        {

            _driver.Navigate().GoToUrl(Constants.URL_BACEN);

            var buttonElemnt = _driver.FindElementByClassName(Constants.BTN_BACK);
            buttonElemnt.Click();

            Thread.Sleep(2000);

            var html = _driver.PageSource;
            _document.LoadHtml(html);

            var top10 = BuildTop10BancosEFinanceiras(_document);

            return top10;

        }

        private static Top3BF BuildTop3BancosEFinanceiras(HtmlNodeCollection collection)
        {
            var top3 = new Top3BF() { BancosFinanceiras = new BancosEFinanceiras[Constants.AMOUNT_OBJECTS_THREE] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS_THREE; i++)
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
            var top3 = new Top3BF() { BancosFinanceiras = new BancosEFinanceiras[Constants.AMOUNT_OBJECTS_THREE] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS_THREE; i++)
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
            var top3 = new Top3Reclamacoes() { Reclamacoes = new Reclamacao[Constants.AMOUNT_OBJECTS_THREE] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS_THREE; i++)
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
            var top3 = new Top03AdmConsorcio() { AdministradorasConsorcio = new AdmConsorcio[Constants.AMOUNT_OBJECTS_THREE] };

            for (int i = 0; i < Constants.AMOUNT_OBJECTS_THREE; i++)
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

        private static Top10BF BuildTop10BancosEFinanceiras(HtmlDocument htmlDocument)
        {
            var top10 = new Top10BF() { BancosFinanceiras = new BancosEFinanceiras[Constants.AMOUNT_OBJECTS_TEN] };
            var size = 0;
            var participantsArray = new string[size];

            for (int i = 0; i < Constants.AMOUNT_OBJECTS_TEN; i++)
            {
                var position = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[1]/span[1]");
                var institutionName = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[2]/span[1]");
                var index = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[3]/span[1]");
                var claims = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[4]/a[1]/span[1]");
                var customersAmount = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[5]/span[1]");
                var othersClaimsRegistred = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[2]/td[2]/a[1]/span[1]");
                var othersClaimsNotRegistred = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[3]/td[2]/a[1]/span[1]");
                var totalClaims = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[4]/td[2]/a[1]/span[1]");

                var participantsTable = htmlDocument.DocumentNode.SelectNodes($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[1]/table[1]");

                if(participantsTable[0].ChildNodes.Count <= 3)
                {
                    participantsTable = null;
                }
                else
                {
                    size = participantsTable[0].ChildNodes[3].ChildNodes.Count - 1;
                    participantsArray = new string[size];
                }                

                if (participantsTable is not null)
                {
                    for (int j = 0; j < size; j++)
                    {
                        var participant = participantsTable[0].ChildNodes[3].ChildNodes[j].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText.Trim();

                        participantsArray[j] = participant;
                    }
                }

                top10.BancosFinanceiras[i] = new BancosEFinanceiras
                {
                    Posicao = position.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    InstituicaoFinanceira = institutionName.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Indice = index.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Participantes = participantsArray.Length <= 0 ? null : participantsArray,
                    QuantidadeClientes = customersAmount.InnerText.Trim(),
                    ReclamacoesNaoReguladas = othersClaimsNotRegistred.InnerText.Trim(),
                    ReclamacoesReguladasOutras = othersClaimsRegistred.InnerText.Trim(),
                    ReclamacoesReguladasProcedentes = claims is null ? "0" : claims.InnerText.Trim(),
                    TotalReclamacoes = totalClaims.InnerText.Trim()
                };
            }

            return top10;
        }

        #endregion

    }
}

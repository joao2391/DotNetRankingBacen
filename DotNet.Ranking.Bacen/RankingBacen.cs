using System;
using System.Collections.Generic;
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
        private readonly int _timeToSleep = 2000;

        public RankingBacen(IHttpClientWrapper clientFactory)
        {
            _httpClient = clientFactory;
        }

        public RankingBacen(string chromeDriverPath, ChromeOptions driverOptions, int timeToSleep)
        {
            if (string.IsNullOrEmpty(chromeDriverPath)) 
            {
                throw new ArgumentException("chromeDriverPath cannot be null");
            }

            var driver = new ChromeDriver(chromeDriverPath, driverOptions);
            _driver = driver;
            _timeToSleep = timeToSleep;
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

        public DemaisBancosEFinanceiras GetDemaisBancosEFinanceiras()
        {
            try
            {
                var result = GetAllDemaisBancosEFinanceiras();

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

        public TodasReclamacoes GetTodasReclamacoes()
        {
            try
            {
                var result = GetTodasReclamacoesDoBuildTodasReclamacoes();

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

        public TodasAdmConsorcio GetTodasAdmsConsorcio()
        {
            try
            {
                var result = GetTodasAdmConsorcioDoBuildTodasAdmConsorcio();

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

            var botaoListaCompleta = _driver.FindElementByClassName(Constants.BTN_BACK);
            botaoListaCompleta.Click();

            //Aguarda o tempo fornecido até que a página esteja
            // totalmente carregada.
            Thread.Sleep(_timeToSleep);

            var html = _driver.PageSource;
            _document.LoadHtml(html);

            var top10 = BuildTop10BancosEFinanceiras(_document);

            return top10;

        }

        private DemaisBancosEFinanceiras GetAllDemaisBancosEFinanceiras()
        {            
            _driver.Navigate().GoToUrl(Constants.URL_BACEN);

            var botaoListaCompleta = _driver.FindElementByXPath(Constants.XPATH_ALL_DBF);
            botaoListaCompleta.Click();
            
            //Aguarda o tempo fornecido até que a página esteja
            // totalmente carregada.
            Thread.Sleep(_timeToSleep);

            var html = _driver.PageSource;
            _document.LoadHtml(html);

            var demaisBancos = BuildDemaisBancosEFinanceiras(_document);

            return demaisBancos;
            
        }

        private TodasReclamacoes GetTodasReclamacoesDoBuildTodasReclamacoes()
        {
            _driver.Navigate().GoToUrl(Constants.URL_BACEN);

            var botaoListaCompleta = _driver.FindElementByXPath(Constants.XPATH_ALL_CLAIMS);
            botaoListaCompleta.Click();

            //Aguarda o tempo fornecido até que a página esteja
            // totalmente carregada.
            Thread.Sleep(_timeToSleep);

            var html = _driver.PageSource;
            _document.LoadHtml(html);

            var todasReclamacoes = BuildTodasReclamacoes(_document);

            return todasReclamacoes;
        }

        private TodasAdmConsorcio GetTodasAdmConsorcioDoBuildTodasAdmConsorcio()
        {
            _driver.Navigate().GoToUrl(Constants.URL_BACEN);

            var botaoListaCompleta = _driver.FindElementByXPath(Constants.XPATH_ALL_ADM);
            botaoListaCompleta.Click();

            //Aguarda o tempo fornecido até que a página esteja
            // totalmente carregada.
            Thread.Sleep(_timeToSleep);

            var html = _driver.PageSource;
            _document.LoadHtml(html);

            var todasAdmConsorcio = BuildTodasAdmConsorcio(_document);

            return todasAdmConsorcio;
        }

        private static Top3BF BuildTop3BancosEFinanceiras(HtmlNodeCollection collection)
        {
            var nodesNaPagina = collection[0].ChildNodes[5].ChildNodes;
            // Antes e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 2
            var quantidadeItems = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_TRES : collection[0].ChildNodes[5].ChildNodes.Count - 2;

            var top3 = new Top3BF() { BancosFinanceiras = new BancosEFinanceiras[quantidadeItems] };

            for (int i = 0; i < quantidadeItems; i++)
            {
                var posicao = collection[0].ChildNodes[5].ChildNodes[i+1].ChildNodes[1].ChildNodes[1];
                var nomeInstituicaoFinanceira = collection[0].ChildNodes[5].ChildNodes[i+1].ChildNodes[1].ChildNodes[3];
                var indice = collection[0].ChildNodes[5].ChildNodes[i+1].ChildNodes[1].ChildNodes[5];
                
                top3.BancosFinanceiras[i] = new BancosEFinanceiras
                {
                    Posicao = posicao.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    InstituicaoFinanceira = nomeInstituicaoFinanceira.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Indice = indice.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty)
                    
                };
            }

            return top3;
        }

        private static Top3BF BuildTop3DemaisBancosEFinanceiras(HtmlNodeCollection collection)
        {
            var nodesNaPagina = collection[0].ChildNodes[5].ChildNodes;
            // Antes e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 2
            var quantidadeItems = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_TRES : collection[0].ChildNodes[5].ChildNodes.Count - 2;

            var top3 = new Top3BF() { BancosFinanceiras = new BancosEFinanceiras[quantidadeItems] };

            for (int i = 0; i < quantidadeItems; i++)
            {
                var posicao = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[1];
                var nomeInstituicaoFinanceira = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[3];
                var indice = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[5];
                
                top3.BancosFinanceiras[i] = new BancosEFinanceiras
                {
                    Posicao = posicao.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    InstituicaoFinanceira = nomeInstituicaoFinanceira.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Indice = indice.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty)
                };
            }

            return top3;
        }

        private static Top3Reclamacoes BuildTop3Reclamacoes(HtmlNodeCollection collection)
        {
            var nodesNaPagina = collection[0].ChildNodes[5].ChildNodes;
            // Antes e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 2
            var quantidadeItems = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_TRES : collection[0].ChildNodes[5].ChildNodes.Count - 2;

            var top3 = new Top3Reclamacoes() { Reclamacoes = new Reclamacao[quantidadeItems] };

            for (int i = 0; i < quantidadeItems; i++)
            {
                var posicao = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[1];
                var motivo = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[3];

                top3.Reclamacoes[i] = new Reclamacao
                {
                    Posicao = posicao.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    Motivo = motivo.InnerText.Trim()
                };
            }

            return top3;
        }

        private static Top03AdmConsorcio BuildTop3AdmConsorcio(HtmlNodeCollection collection)
        {
            var nodesNaPagina = collection[0].ChildNodes[5].ChildNodes;
            // Antes e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 2
            var quantidadeItems = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_TRES : collection[0].ChildNodes[5].ChildNodes.Count - 2;

            var top3 = new Top03AdmConsorcio() { AdministradorasConsorcio = new AdmConsorcio[quantidadeItems] };

            for (int i = 0; i < quantidadeItems; i++)
            {
                var posicao = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[1];
                var nomeInstituicaoFinanceira = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[3];
                var indice = collection[0].ChildNodes[5].ChildNodes[i + 1].ChildNodes[1].ChildNodes[5];

                top3.AdministradorasConsorcio[i] = new AdmConsorcio
                {
                    Posicao = posicao.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    Indice = indice.InnerText.Trim(),
                    NomeAdmConsorcio = nomeInstituicaoFinanceira.InnerText.Trim()
                };
            }

            return top3;
        }

        private static Top10BF BuildTop10BancosEFinanceiras(HtmlDocument htmlDocument)
        {
            var nodesNaPagina = htmlDocument.DocumentNode.SelectNodes(Constants.XPATH_NODES_BF);
            // Antes e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 2
            var quantidadeItems = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_DEZ : nodesNaPagina[0].ChildNodes.Count - 2;

            var top10 = new Top10BF() { BancosFinanceiras = new BancosEFinanceiras[quantidadeItems] };
            var tamanho = 0;
            var participantsArray = new string[tamanho];

            for (int i = 0; i < quantidadeItems; i++)
            {
                var posicao = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[1]/span[1]");
                var nomeInstituicaoFinanceira = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[2]/span[1]");
                var indice = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[3]/span[1]");
                var reclamacoes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[4]/a[1]/span[1]");
                var quantidadeClientes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[5]/span[1]");
                var outrasReclamacoesRegistradas = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[2]/td[2]/a[1]/span[1]");
                var outrasReclamacoesNaoRegistradas = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[3]/td[2]/a[1]/span[1]");
                var totalReclamacoes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[4]/td[2]/a[1]/span[1]");

                var participantsTable = htmlDocument.DocumentNode.SelectNodes($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[1]/table[1]");
                // Há situações em que não existem participantes
                // Portanto, faz-se necessário a verificação a seguir
                if (participantsTable[0].ChildNodes.Count <= 3)
                {
                    participantsTable = null;
                }
                else
                {
                    tamanho = participantsTable[0].ChildNodes[3].ChildNodes.Count - 1;
                    participantsArray = new string[tamanho];
                }                

                if (participantsTable is not null)
                {
                    for (int j = 0; j < tamanho; j++)
                    {
                        var participant = participantsTable[0].ChildNodes[3].ChildNodes[j].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText.Trim();

                        participantsArray[j] = participant;
                    }
                }

                top10.BancosFinanceiras[i] = new BancosEFinanceiras
                {
                    Posicao = posicao.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    InstituicaoFinanceira = nomeInstituicaoFinanceira.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Indice = indice.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Participantes = participantsArray.Length <= 0 ? null : participantsArray,
                    QuantidadeClientes = quantidadeClientes.InnerText.Trim(),
                    ReclamacoesNaoReguladas = outrasReclamacoesNaoRegistradas.InnerText.Trim(),
                    ReclamacoesReguladasOutras = outrasReclamacoesRegistradas.InnerText.Trim(),
                    ReclamacoesReguladasProcedentes = reclamacoes is null ? "0" : reclamacoes.InnerText.Trim(),
                    TotalReclamacoes = totalReclamacoes.InnerText.Trim()
                };
            }

            return top10;
        }

        private static DemaisBancosEFinanceiras BuildDemaisBancosEFinanceiras(HtmlDocument htmlDocument)
        {            
            var nodesNaPagina = htmlDocument.DocumentNode.SelectNodes(Constants.XPATH_NODES_DBF);
            // Antes e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 2
            var quantidadeItens = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_VINTE_E_QUATRO : nodesNaPagina[0].ChildNodes.Count - 2;
            var demaisBancos = new DemaisBancosEFinanceiras() { BancosFinanceiras = new BancosEFinanceiras[quantidadeItens] };
            var tamanho = 0;
            var arrayParticipantes = new string[tamanho];

            for (int i = 0; i < quantidadeItens; i++)
            {
                var posicao = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[1]/span[1]");
                var nomeInstituicaoFinanceira = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[2]/span[1]");
                var indice = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[3]/span[1]");
                var reclamacoes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[4]/a[1]/span[1]");
                var quantidadeClientes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[5]/span[1]");
                var outrasReclamacoesRegistradas = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[2]/td[2]/a[1]/span[1]");
                var outrasReclamacoesNaoRegistradas = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[3]/td[2]/a[1]/span[1]");
                var totalReclamacoes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[4]/td[2]/a[1]/span[1]");

                var participantes = htmlDocument.DocumentNode.SelectNodes($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[1]/table[1]");
                // Há situações em que não existem participantes
                // Portanto, faz-se necessário a verificação a seguir
                if (participantes[0].ChildNodes.Count <= 3)
                {
                    participantes = null;
                }
                else
                {
                    tamanho = participantes[0].ChildNodes[3].ChildNodes.Count - 1;
                    arrayParticipantes = new string[tamanho];
                }

                if (participantes is not null)
                {
                    for (int j = 0; j < tamanho; j++)
                    {
                        var participante = participantes[0].ChildNodes[3].ChildNodes[j].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].InnerText.Trim();

                        arrayParticipantes[j] = participante;
                    }
                }

                demaisBancos.BancosFinanceiras[i] = new BancosEFinanceiras
                {
                    Posicao = posicao.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    InstituicaoFinanceira = nomeInstituicaoFinanceira.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Indice = indice.InnerText.Trim().Replace(Constants.BREAKLINE, string.Empty),
                    Participantes = arrayParticipantes.Length <= 0 ? null : arrayParticipantes,
                    QuantidadeClientes = quantidadeClientes.InnerText.Trim(),
                    ReclamacoesNaoReguladas = outrasReclamacoesNaoRegistradas.InnerText.Trim(),
                    ReclamacoesReguladasOutras = outrasReclamacoesRegistradas.InnerText.Trim(),
                    ReclamacoesReguladasProcedentes = reclamacoes is null ? "0" : reclamacoes.InnerText.Trim(),
                    TotalReclamacoes = totalReclamacoes.InnerText.Trim()
                };
            }

            return demaisBancos;
        }

        private static TodasReclamacoes BuildTodasReclamacoes(HtmlDocument htmlDocument)
        {            
            var nodesNaPagina = htmlDocument.DocumentNode.SelectNodes(Constants.XPATH_NODES_REC);
            // Antes ou/e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 1
            var quantidadeItens = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_OITENTA_E_QUATRO : nodesNaPagina[0].ChildNodes.Count - 1;

            var reclamacoes = new TodasReclamacoes()
            {
                Reclamacoes = new Reclamacao[quantidadeItens]
            };
            
            for (int i = 0; i < quantidadeItens; i++)
            {
                var posicao = htmlDocument.DocumentNode.SelectSingleNode($"/html/body/div/form/div[4]/div[2]/span[{i + 1}]/h3/div[1]");
                var reclamacao = htmlDocument.DocumentNode.SelectSingleNode($"/html/body/div/form/div[4]/div[2]/span[{i + 1}]/h3/div[2]/a[1]/span[1]");                
                var quantidade = htmlDocument.DocumentNode.SelectSingleNode($"/html/body/div/form/div[4]/div[2]/span[{i + 1}]/h3/div[3]");
                // Há bugs na lista que não exibem o número da posição da reclamção
                // Portanto, faz-se necessário essa variável de controle.
                var posicaoSubstituta = i + 1;

                reclamacoes.Reclamacoes[i] = new Reclamacao()
                {
                    Posicao = posicao is null ? posicaoSubstituta.ToString() : posicao.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    Motivo = reclamacao?.InnerText,
                    Quantidade = Convert.ToInt32(Convert.ToDecimal(quantidade?.InnerText)),
                    Instituicoes = new List<BancosEFinanceiras>()
                };


                var instituicoesFinanceiras = htmlDocument.DocumentNode.SelectNodes($"/html[1]/body[1]/div[1]/form[1]/div[4]/div[2]/span[{i + 1}]/div[1]/span[1]");

                for (int j = 0; j < instituicoesFinanceiras?[0]?.ChildNodes?.Count; j++)
                {
                    if (j >= 5 && instituicoesFinanceiras[0]?.ChildNodes[j]?.ChildNodes.Count > 0)
                    {
                        var nomeInstituicaoNoHtml = instituicoesFinanceiras[0]?.ChildNodes[j]?.ChildNodes[1]?.ChildNodes[3]?.ChildNodes[0];

                        string nomeInstituicaoFinanceira;
                        // Por algum motivo, algumas instituições não estão como link
                        // Portanto, faz-se necessário a verificação a seguir
                        if (nomeInstituicaoNoHtml?.InnerText == "\r\n")
                        {
                            var instituicaoFinanceira = instituicoesFinanceiras[0]?.ChildNodes[j]?.ChildNodes[1]?.ChildNodes[3]?.ChildNodes[1];
                            nomeInstituicaoFinanceira = instituicaoFinanceira is null ? string.Empty : instituicaoFinanceira.InnerText;
                        }
                        else
                        {
                            nomeInstituicaoFinanceira = nomeInstituicaoNoHtml?.InnerText;
                        }

                        var quantidadeReclamacoesNoHtml = instituicoesFinanceiras[0]?.ChildNodes[j]?.ChildNodes[2]?.ChildNodes[3]?.ChildNodes[0];
                        string quantidadeReclamacoes = quantidadeReclamacoesNoHtml is null ? string.Empty : quantidadeReclamacoesNoHtml.InnerText;

                        reclamacoes.Reclamacoes[i].Instituicoes.Add(new BancosEFinanceiras()
                                                    {
                                                        InstituicaoFinanceira = nomeInstituicaoFinanceira.Trim(),
                                                        TotalReclamacoes = quantidadeReclamacoes
                                                        
                                                    }
                        );
                        
                    }

                }
            }


            return reclamacoes;
        }

        private static TodasAdmConsorcio BuildTodasAdmConsorcio(HtmlDocument htmlDocument)
        {
            var nodesNaPagina = htmlDocument.DocumentNode.SelectNodes(Constants.XPATH_NODES_ADM);
            // Antes e depois dos itens, há um #text
            // Portanto, faz-se necessário subtrair 2
            var quantidadeItens = nodesNaPagina is null ? Constants.QUANTIDADE_OBJETOS_TRINTA_E_CINCO : nodesNaPagina[0].ChildNodes.Count - 2;

            var admConsorcio = new TodasAdmConsorcio()
            {
                AdmsConsorcio = new AdmConsorcio[quantidadeItens]
            };

            for (int i = 0; i < quantidadeItens; i++)
            {
                var posicao = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[1]/span[1]");

                var nomeAdmConsorcio = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[2]/span[1]");

                var indice = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[3]/span[1]");

                var reclamacoesProcedentes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[4]/a[1]/span[1]");

                var consorciados = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/h3[1]/div[5]/span[1]");                
                
                // Quando não há reclamacoes, o xpath muda devido à ausência do link no número
                // Portanto, faz-se necessário os IFs a seguir
                var reclamacoesReguladasOutras = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[2]/td[2]/a[1]/span[1]");
                if (reclamacoesReguladasOutras is null)
                {
                    reclamacoesReguladasOutras = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[2]/td[2]/span[1]/span[1]");
                }

                var reclamacoesNaoReguladas = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[3]/td[2]/a[1]/span[1]");
                if (reclamacoesNaoReguladas is null)
                {
                    reclamacoesNaoReguladas = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[3]/td[2]/span[1]/span[1]");
                }

                var totalReclamacaoes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[4]/td[2]/a[1]/span[1]");
                if (totalReclamacaoes is null)
                {
                    totalReclamacaoes = htmlDocument.DocumentNode.SelectSingleNode($"/html[1]/body[1]/div[1]/form[1]/div[4]/span[1]/span[1]/div[2]/span[{i + 1}]/div[1]/div[2]/table[1]/tbody[1]/tr[4]/td[2]/span[1]/span[1]");
                }


                admConsorcio.AdmsConsorcio[i] = new AdmConsorcio()
                {
                    Indice = indice?.InnerText.Trim(),
                    NomeAdmConsorcio = nomeAdmConsorcio?.InnerText.Trim(),
                    Posicao = posicao?.InnerText.Trim().Replace(Constants.SPACE, string.Empty),
                    ReclamacoesNaoReguladas = Convert.ToInt32(reclamacoesNaoReguladas?.InnerText.Trim()),
                    ReclamacoesReguladasOutras = Convert.ToInt32(reclamacoesReguladasOutras?.InnerText.Trim()),
                    ReclamacoesReguladasProcedentes = Convert.ToInt32(reclamacoesProcedentes?.InnerText.Trim()),
                    TotalReclamacoes = Convert.ToInt32(Convert.ToDecimal(totalReclamacaoes?.InnerText.Trim()))
                };

            }

            return admConsorcio;
        }


        #endregion

    }
}

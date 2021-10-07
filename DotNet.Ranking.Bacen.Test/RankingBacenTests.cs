using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using OpenQA.Selenium.Chrome;

namespace DotNet.Ranking.Bacen.Test
{
    public class RankingBacenTests
    {
        private RankingBacen _rankingBacen;
        private readonly string fakeContent = string.Empty;
        
        public RankingBacenTests()
        {
            fakeContent = File.ReadAllText(@".\FakeData.html");
        }


        [Fact]
        public async Task Should_Return_Top03_Bancos_e_Financeiras_From_GetTop03BancosEFinanceiras()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync()).ReturnsAsync(response);

            _rankingBacen = new RankingBacen(mockHttp.Object);

            var result = await _rankingBacen.GetTop03BancosEFinanceirasAsync();

            Assert.NotNull(result);
            Assert.IsType<Top3BF>(result);
            Assert.Equal(Constants.TRES, result.BancosFinanceiras.Length);
            Assert.Equal(Constants.INSTI_BF, result.BancosFinanceiras[0].InstituicaoFinanceira);

        }

        [Fact]
        public async Task Should_Return_Top03_Demais_Bancos_e_Financeiras_From_GetTop03DemaisBancosEFinanceiras()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync()).ReturnsAsync(response);

            _rankingBacen = new RankingBacen(mockHttp.Object);

            var result = await _rankingBacen.GetTop03DemaisBancosEFinanceirasAsync();

            Assert.NotNull(result);
            Assert.IsType<Top3BF>(result);
            Assert.Equal(Constants.TRES, result.BancosFinanceiras.Length);
            Assert.Equal(Constants.INSTI_DBF, result.BancosFinanceiras[0].InstituicaoFinanceira);

        }

        [Fact]
        public async Task Should_Return_Top03_Reclamacoes_From_GetTop03Reclamacoes()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync()).ReturnsAsync(response);

            _rankingBacen = new RankingBacen(mockHttp.Object);

            var result = await _rankingBacen.GetTop03ReclamacoesAsync();

            Assert.NotNull(result);
            Assert.IsType<Top3Reclamacoes>(result);
            Assert.Equal(Constants.TRES, result.Reclamacoes.Length);
            Assert.Equal(Constants.MOTIVO_RECLAMACAO, result.Reclamacoes[0].Motivo);

        }

        [Fact]
        public async Task Should_Return_Top03_AdministradorasConsorcio_From_GetTop03AdmConsorcio()
        {
            var mockHttp = new Mock<IHttpClientWrapper>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(fakeContent)
            };

            mockHttp.Setup(x => x.GetAsync()).ReturnsAsync(response);

            _rankingBacen = new RankingBacen(mockHttp.Object);

            var result = await _rankingBacen.GetTop03AdmConsorcioAsync();

            Assert.NotNull(result);
            Assert.IsType<Top03AdmConsorcio>(result);
            Assert.Equal(Constants.TRES, result.AdministradorasConsorcio.Length);
            Assert.Equal(Constants.NOME_ADM, result.AdministradorasConsorcio[0].NomeAdmConsorcio);            

        }

        [Fact]
        public void Should_Return_Top10_Bancos_E_Financeiras_From_GetTop10BancosEFinanceirasAsync()
        {
            var mockChromeOptions = new Mock<ChromeOptions>();

            _rankingBacen = new RankingBacen(mockChromeOptions.Object, "");

            var result = _rankingBacen.GetTop10BancosEFinanceiras();

            Assert.NotNull(result);
            Assert.IsType<Top10BF>(result);
            //Assert.Equal(Constants.DEZ, result.AdministradorasConsorcio.Length);
            //Assert.Equal(Constants.NOME_ADM, result.AdministradorasConsorcio[0].NomeAdmConsorcio);

        }
    }
}

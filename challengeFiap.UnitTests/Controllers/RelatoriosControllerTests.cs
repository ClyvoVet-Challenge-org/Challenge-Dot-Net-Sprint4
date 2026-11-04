using challengeFiap.Controllers;
using challengeFiap.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace challengeFiap.UnitTests.Controllers
{
    public class RelatoriosControllerTests
    {
        private readonly Mock<IRelatorioService> _relatorioServiceMock;
        private readonly RelatoriosController _controller;

        public RelatoriosControllerTests()
        {
            _relatorioServiceMock = new Mock<IRelatorioService>();
            _controller = new RelatoriosController(_relatorioServiceMock.Object);
        }

        [Fact]
        public async Task ResumoVeterinarios_RetornaOk()
        {
            var resultado = new List<string> { "Resumo" };
            _relatorioServiceMock
                .Setup(service => service.ResumoConsultasVetAsync())
                .ReturnsAsync(resultado);

            var response = await _controller.ResumoVeterinarios();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.ResumoConsultasVetAsync(), Times.Once);
        }

        [Fact]
        public async Task RelatorioEspecies_RetornaOk()
        {
            var resultado = new List<string> { "Canino" };
            _relatorioServiceMock
                .Setup(service => service.RelatorioEspecieAsync())
                .ReturnsAsync(resultado);

            var response = await _controller.RelatorioEspecies();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.RelatorioEspecieAsync(), Times.Once);
        }

        [Fact]
        public async Task RelatorioClinicas_RetornaOk()
        {
            var resultado = new List<string> { "Clínica" };
            _relatorioServiceMock
                .Setup(service => service.ConsultasClinicasAsync())
                .ReturnsAsync(resultado);

            var response = await _controller.RelatorioClinicas();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.ConsultasClinicasAsync(), Times.Once);
        }

        [Fact]
        public async Task ConsultasRealizadas_RetornaOk()
        {
            var resultado = new List<string> { "Consulta realizada" };
            _relatorioServiceMock
                .Setup(service => service.ConsultasRealizadasAsync())
                .ReturnsAsync(resultado);

            var response = await _controller.ConsultasRealizadas();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.ConsultasRealizadasAsync(), Times.Once);
        }

        [Fact]
        public async Task RelatorioOcorrencias_RetornaOk()
        {
            var resultado = new List<string> { "Ocorrência" };
            _relatorioServiceMock
                .Setup(service => service.RelatorioOcorrenciasAsync())
                .ReturnsAsync(resultado);

            var response = await _controller.RelatorioOcorrencias();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.RelatorioOcorrenciasAsync(), Times.Once);
        }

        [Fact]
        public async Task MoradiaAnimalTutor_RetornaOk()
        {
            var resultado = new List<string> { "Mesma moradia" };
            _relatorioServiceMock
                .Setup(service => service.VerificarMoradiaAnimalTutorAsync())
                .ReturnsAsync(resultado);

            var response = await _controller.MoradiaAnimalTutor();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.VerificarMoradiaAnimalTutorAsync(), Times.Once);
        }

        [Fact]
        public async Task ConsultasVeterinario_RetornaOk()
        {
            var nomeVeterinario = "Dr. Silva";
            var resultado = new List<string> { "Consulta do veterinário" };
            _relatorioServiceMock
                .Setup(service => service.ProcurarVetConsultasAsync(nomeVeterinario))
                .ReturnsAsync(resultado);

            var response = await _controller.ConsultasVeterinario(nomeVeterinario);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.ProcurarVetConsultasAsync(nomeVeterinario), Times.Once);
        }

        [Fact]
        public async Task InformacoesAnimal_RetornaOk()
        {
            var nomeAnimal = "Rex";
            var resultado = new List<string> { "Informações do animal" };
            _relatorioServiceMock
                .Setup(service => service.ProcurarInformacaoAnimalAsync(nomeAnimal))
                .ReturnsAsync(resultado);

            var response = await _controller.InformacoesAnimal(nomeAnimal);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.ProcurarInformacaoAnimalAsync(nomeAnimal), Times.Once);
        }

        [Fact]
        public async Task RelatorioVetConsulta_RetornaOk()
        {
            var idVeterinario = 1;
            var resultado = new List<string> { "Relatório do veterinário" };
            _relatorioServiceMock
                .Setup(service => service.RelatorioVetConsultaAsync(idVeterinario))
                .ReturnsAsync(resultado);

            var response = await _controller.RelatorioVetConsulta(idVeterinario);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Same(resultado, okResult.Value);
            _relatorioServiceMock.Verify(service => service.RelatorioVetConsultaAsync(idVeterinario), Times.Once);
        }
    }
}

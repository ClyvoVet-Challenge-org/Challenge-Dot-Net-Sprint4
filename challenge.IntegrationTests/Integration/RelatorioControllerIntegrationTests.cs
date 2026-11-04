using challenge.IntegrationTests.FactoryFixture;
using System.Net;
using System.Net.Http.Json;

namespace challenge.IntegrationTests.Integration
{
    [Collection("ApiCollection")]
    public class RelatorioControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public RelatorioControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ResumoVeterinarios_RetornaOk()
        {
            var response = await _client.GetAsync("api/Relatorios/resumo-veterinarios");
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Relatório não encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task RelatorioEspecies_RetornaOk()
        {
            var response = await _client.GetAsync("api/Relatorios/especies");
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Relatório não encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task RelatorioClinicas_RetornaOk()
        {
            var response = await _client.GetAsync("api/Relatorios/clinicas");
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Relatório não encontrado");
            }
    
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task ConsultasRealizadas_RetornaOk()
        {
            var response = await _client.GetAsync("api/Relatorios/consultas-realizadas");
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Relatório não encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task RelatorioOcorrencias_RetornaOk()
        {
            var response = await _client.GetAsync("api/Relatorios/ocorrencias");
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Relatório não encontrado");
            }
    
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task MoradiaAnimalTutor_RetornaOk()
        {
            var response = await _client.GetAsync("api/Relatorios/moradia-animal-tutor");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Relatório não encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task ConsultasVeterinario_RetornaOk()
        {
            var nomeVeterinario = "Dr. Silva";
            var response = await _client.GetAsync($"api/Relatorios/veterinario/{Uri.EscapeDataString(nomeVeterinario)}");
            
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Nome do veterinário não encontrado");
            }
    
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task InformacoesAnimal_RetornaOk()
        {
            var nomeAnimal = "Mimi";
            var response = await _client.GetAsync($"api/Relatorios/animal/{Uri.EscapeDataString(nomeAnimal)}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Nome do animal não encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }

        [Fact]
        public async Task RelatorioVetConsulta_RetornaOk()
        {
            var idVeterinario = 51;
            var response = await _client.GetAsync($"api/Relatorios/veterinario/consultas/{idVeterinario}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Id nao encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var relatorio = await response.Content.ReadFromJsonAsync<List<string>>();

            Assert.NotNull(relatorio);
        }
    }
}

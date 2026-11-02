using challenge.IntegrationTests.FactoryFixture;
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Dto;
using System.Net;
using System.Net.Http.Json;
using challengeFiap.Domain;

namespace challenge.IntegrationTests.Integration
{
    //Atenção!!! Tanto o create quanto o update precisam ter cuidado com os dados que serão adicionados, porque, se não, pode dar erro.          
    //Recomendação faz um a cada vez, não faz eles tudo junto
    [Collection("ApiCollection")]
    public class _1VetControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public _1VetControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateVeterinario_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_vet = Random.Shared.Next(1,100);
            
            var novoVeterinario = new
            {
                Id_vet = id_vet,
                Nm_vet = "le",
                Cpf_vet = "12121212121",
                Crmv_vet = "22121-SP",
                Email_vet = "212112@gmail.com",
                Senha_vet = "11111"
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/veterinarios/criar/veterinario",novoVeterinario);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var veterinarioCriado = await response.Content.ReadFromJsonAsync<Veterinario>();

            Assert.NotNull(veterinarioCriado);
            Assert.Equal(id_vet, veterinarioCriado.Id_vet);
        }

        [Fact]
        public async Task UpdateVeterinario_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_veterinario = 6;

            var veterinarioAtualizado = new
            {
                Id_vet = id_veterinario,
                Nm_vet = "nome",
                Cpf_vet = "232222",
                Crmv_vet = "2222222",
                Email_vet = "safdf2@gmail.com",
                Senha_vet = "22222"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"api/veterinarios/atualizar/veterinario/{id_veterinario}",veterinarioAtualizado);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi atualizado por: {erro}");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
       
        }

        [Fact]
        public async Task GetVet_dados_RetornaOk()
        {
            // Arrange
            var id_veterinario = 6;

            // Act
            var response = await _client.GetAsync($"api/veterinarios/relatorio/veterinario/{id_veterinario}");

            // Assert
            if (response.StatusCode== HttpStatusCode.NotFound)
            {
                Assert.Fail($"Veterinário não encontrado");
            }
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vet = await response.Content.ReadFromJsonAsync<VeterinarioDTO>();

            Assert.NotNull(vet);
            Assert.Equal(id_veterinario, vet.Veterinario.Id_vet);
            Assert.Contains(vet.Dto, link => link.Rel == "self" && link.Method == "GET");
        }

        [Fact]
        public async Task GetAll_dados_RetornarOk()
        {

            //Act
            var response = await _client.GetAsync("api/veterinarios/relatorio/veterinario");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var vet = await response.Content.ReadFromJsonAsync<PagedResponse<VeterinarioDTO>>();

            Assert.NotNull(vet);
            Assert.All(vet.Items, item =>
                Assert.Contains(item.Dto, link => link.Rel == "self" && link.Method == "GET"));
        }

        [Fact]
        public async Task DeleteVet_dados_RetornaOk()
        {
            // Arrange
            var id_veterinario = 55;

            // Act
            var response = await _client.DeleteAsync($"api/veterinarios/deleta/veterinario/{id_veterinario}");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erroAchado = await response.Content.ReadAsStringAsync();

                Assert.Fail($"Não foi deletado pelo morivo: {erroAchado}");
            }

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}

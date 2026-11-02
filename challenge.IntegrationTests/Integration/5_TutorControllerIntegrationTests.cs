using challenge.IntegrationTests.FactoryFixture;
using challengeFiap.Domain;
using challengeFiap.Domain.Dto;
using challengeFiap.Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace challenge.IntegrationTests.Integration
{
    //Atenção!!! Tanto o create quanto o update precisam ter cuidado com os dados que serão adicionados, porque, se não, pode dar erro.          
    //Recomendação faz um a cada vez, não faz eles tudo junto
    [Collection("ApiCollection")]
    public class _5TutorControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public _5TutorControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CriarTutor_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_tutor = Random.Shared.Next(1, 100);

            var novoTutor = new
            {
                Id_tutor = id_tutor,
                Cpf_tutor = "11122345",
                Nm_tutor = "carol",
                Nr_telefone_tutor = "11888999"
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/tutor/criar/Tutor",novoTutor);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var tutorCriado = await response.Content.ReadFromJsonAsync<Tutor>();
            Assert.NotNull(tutorCriado);
            Assert.Equal(id_tutor, tutorCriado.Id_tutor);
        }

        [Fact]
        public async Task AtualizarTutor_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_tutor = 1;

            var tutorAtualizado = new
            {
                Id_tutor = id_tutor,
                Cpf_tutor = "987654321",
                Nm_tutor = "lUANA",
                Nr_telefone_tutor = "112345678"
            };

            // Act
            var response = await _client.PutAsJsonAsync($"api/tutor/atualizar/Tutor/{id_tutor}",tutorAtualizado);

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
        public async Task GetTutor_dados_RetornaOk()
        {
            // Arrange
            var id_tutor = 3;

            // Act
            var response = await _client.GetAsync($"api/tutor/relatorio/Tutor/{id_tutor}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Id nao encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var tutor = await response.Content.ReadFromJsonAsync<TutorDTO>();

            Assert.NotNull(tutor);
            Assert.Equal(id_tutor, tutor.Tutor.Id_tutor);
            Assert.Contains(tutor.Dto, link => link.Rel == "self" && link.Method == "GET");
        }

        [Fact]
        public async Task GetAllTutor_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/tutor/relatorio/Tutor");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var tutors = await response.Content.ReadFromJsonAsync<PagedResponse<TutorDTO>>();

            Assert.NotNull(tutors);
            Assert.All(tutors.Items, item =>
                Assert.Contains(item.Dto, link => link.Rel == "self" && link.Method == "GET"));
        }

        [Fact]
        public async Task DeleteTutor_dados_RetornaOk()
        {
            // Arrange
            var id_tutor = 95;

            // Act
            var response = await _client.DeleteAsync($"api/tutor/deleta/Tutor/{id_tutor}");

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

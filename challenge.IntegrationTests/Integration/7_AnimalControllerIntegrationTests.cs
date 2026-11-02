using challenge.IntegrationTests.FactoryFixture;
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Dto;
using System;
using System.Net;
using System.Net.Http.Json;
using challengeFiap.Domain;

namespace challenge.IntegrationTests.Integration
{
    //Atenção!!! Tanto o create quanto o update precisam ter cuidado com os dados que serão adicionados, porque, se não, pode dar erro.          
    //Recomendação faz um a cada vez, não faz eles tudo junto
    [Collection("ApiCollection")]
    public class _7AnimalControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public _7AnimalControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateAnimal_DadosValidos_RetornaCreated()
        {
            // Arrange
            var id_animal = Random.Shared.Next(1, 100);
            var novoAnimal = new
            {
                Id_animal = id_animal,
                Rg_animal = "1122345",
                Nr_microchip_animal = "1111223",
                Nm_animal = "MEK",
                Dt_nascimento_animal = DateTime.Now,
                Peso_animal = 1,
                Especie_animal = "GATO",
                Raca_animal = "SIAMES",
                Id_tutor = 52
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/animals/criar/animal",novoAnimal);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var animalCriado = await response.Content.ReadFromJsonAsync<Animal>();

            Assert.NotNull(animalCriado);
            Assert.Equal(id_animal, animalCriado.Id_animal);
        }

        [Fact]
        public async Task UpdateAnimal_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_animal = 94;

            var animalAtualizado = new
            {
                Id_animal = id_animal,
                Rg_animal = "123456789",
                Nr_microchip_animal = "123123123",
                Nm_animal = "mel",
                Dt_nascimento_animal = DateTime.Now,
                Peso_animal = 1,
                Especie_animal = "Cachorro",
                Raca_animal = "Labrador",
                Id_tutor = 52
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/api/animals/atualizar/animal/{id_animal}",animalAtualizado);

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
        public async Task GetAnimal_dados_RetornaOk()
        {
            // Arrange
            var id_animal = 94;

            // Act
            var response = await _client.GetAsync($"/api/Animals/relatorio/animal/{id_animal}");
            
            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Id nao encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var animal = await response.Content.ReadFromJsonAsync<AnimalDTO>();

            Assert.NotNull(animal);
            Assert.Equal(id_animal, animal.Animal.Id_animal);
            Assert.Contains(animal.Dto, link => link.Rel == "self" && link.Method == "GET");
        }

        [Fact]
        public async Task GetAllAnimal_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("/api/animals/relatorio/animal");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var animals = await response.Content.ReadFromJsonAsync<PagedResponse<AnimalDTO>>();

            Assert.NotNull(animals);
            Assert.All(animals.Items, item =>
                Assert.Contains(item.Dto, link => link.Rel == "self" && link.Method == "GET"));
        }

        [Fact]
        public async Task DeleteAnimal_dados_RetornaOk()
        {
            // Arrange
            var id_animal = 34;

            // Act
            var response = await _client.DeleteAsync($"/api/animals/deleta/animal/{id_animal}");

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
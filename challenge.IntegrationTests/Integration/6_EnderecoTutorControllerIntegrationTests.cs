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
    public class _6EnderecoTutorControllerIntegrationTests
    {
        private readonly HttpClient _client;

        public _6EnderecoTutorControllerIntegrationTests(ApiFactoryFixture factory)
        {
            _client = factory.CreateClient();
        }

        //Parte de teste de cricao 
        [Fact]
        public async Task CriarEnderecoTutor_DadosValidos_RetornaOk()
        {
            // Arrange
            var id_endereco_tutor = Random.Shared.Next(1, 100);

            var novoEnderecoTutor = new
            {
                Id_endereco_tutor = id_endereco_tutor,
                Pais = "Brasil",
                Estado = "SP",
                Cidade = "São Paulo",
                Bairro = "Bairro amarelo",
                Logradouro_rua = "Rua Dom coelho",
                Nr_rua = "11111111",
                Complemento = "casa 122",
                Cep = "1918256",
                Id_tutor = 52
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/enderecotutors/criar/enderecoresponsavel",novoEnderecoTutor);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Assert.Fail($"Não foi criado por: {erro}");
            }


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var enderecoTutorCriado = await response.Content.ReadFromJsonAsync<EnderecoTutor>();
            Assert.NotNull(enderecoTutorCriado);
            Assert.Equal(id_endereco_tutor,enderecoTutorCriado.Id_endereco_tutor);
        }

        [Fact]
        public async Task UpdateEnderecoTutor_DadosValidos_RetornaSucesso()
        {
            // Arrange
            var id_enderecoTutor = 3;

            var enderecoTutorAtualizado = new
            {
                Id_endereco_tutor = id_enderecoTutor,
                Pais = "Brasil",
                Estado = "SP",
                Cidade = "São Paulo",
                Bairro = "Bairro Roxo",
                Logradouro_rua = "Rua Dom",
                Nr_rua = "12",
                Complemento = "Casa 2",
                Cep = "01234567",
                Id_tutor = 3
            };

            // Act
            var response = await _client.PutAsJsonAsync($"api/enderecotutors/atualizar/enderecoresponsavel/{id_enderecoTutor}", enderecoTutorAtualizado);

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
        public async Task GetEnderecoTutor_dados_RetornaOk()
        {
            // Arrange
            var id_endereco_tutor = 58;

            // Act
            var response = await _client.GetAsync($"api/enderecotutors/relatorio/enderecoresponsavel/{id_endereco_tutor}");

            // Assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Assert.Fail("Id nao encontrado");
            }

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var endereco = await response.Content.ReadFromJsonAsync<EnderecoTutorDTO>();

            Assert.NotNull(endereco);
            Assert.Equal(id_endereco_tutor, endereco.EnderecoTutor.Id_endereco_tutor);
            Assert.Contains(endereco.Dto, link => link.Rel == "self" && link.Method == "GET");
        }

        [Fact]
        public async Task GetAllEnderecoTutor_dados_RetornarOk()
        {
            //Act
            var response = await _client.GetAsync("api/enderecotutors/relatorio/enderecoresponsavel");

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var enderecos = await response.Content.ReadFromJsonAsync<PagedResponse<EnderecoTutorDTO>>();

            Assert.NotNull(enderecos);
            Assert.All(enderecos.Items, item =>
                Assert.Contains(item.Dto, link => link.Rel == "self" && link.Method == "GET"));
        }

        [Fact]
        public async Task DeleteEnderecoTutor_dados_RetornaOk()
        {
            // Arrange
            var id_endereco_tutor = 1;

            // Act
            var response = await _client.DeleteAsync($"api/enderecotutors/deleta/enderecoresponsavel/{id_endereco_tutor}");

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

using challengeFiap.Application.Diagnostics;
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using challengeFiap.Domain.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace challengeFiap.Application.Service
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalOracleRepository _animalOracle;
        private readonly IAnimalMongoRepository _animalMongo;
        private readonly ILogger<AnimalService> _logger;

        public AnimalService(
            IAnimalOracleRepository animalOracle,
            IAnimalMongoRepository animalMongo,
            ILogger<AnimalService> logger)
        {
            _animalOracle = animalOracle;
            _animalMongo = animalMongo;
            _logger = logger;
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            _logger.LogInformation("Criando animal.");
            var createdAnimal = await _animalOracle.CreateAnimalAsync(animal);
            await _animalMongo.CreateAnimalAsync(createdAnimal);
            return createdAnimal;
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            _logger.LogInformation("Buscando todos os animais.");
            var oracleItems = await _animalOracle.GetAllAnimalsAsync();
            var mongoItems = await _animalMongo.GetAllAnimalsAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_animal);
        }

        public Task<Animal> GetAnimalAsync(int id_animal)
        {
            _logger.LogInformation("Buscando animal por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _animalOracle.GetAnimalAsync(id_animal),
                () => _animalMongo.GetAnimalAsync(id_animal));
        }

        public async Task<Animal> UpdateAnimalAsync(int id_animal, Animal animal)
        {
            _logger.LogInformation("Atualizando animal.");
            var updatedAnimal = await _animalOracle.UpdateAnimalAsync(id_animal, animal);
            await _animalMongo.UpdateAnimalAsync(id_animal, updatedAnimal);
            return updatedAnimal;
        }

        public async Task<Animal> DeleteAnimalAsync(int id_animal)
        {
            _logger.LogInformation("Excluindo animal.");
            var deletado = await _animalOracle.DeleteAnimalAsync(id_animal);
            await _animalMongo.DeleteAnimalAsync(id_animal);
            return deletado;
        }
    }
}

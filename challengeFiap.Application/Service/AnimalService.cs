using challengeFiap.Application.Diagnostics;
using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.MongoRepository;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;
using challengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using challengeFiap.Application.InterfacesService;

namespace challengeFiap.Application.Service
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalOracleRepository _animalOracle;
        private readonly IAnimalMongoRepository _animalMongo;

        public AnimalService(IAnimalOracleRepository animalOracle, IAnimalMongoRepository animalMongo)
        {
            _animalOracle = animalOracle;
            _animalMongo = animalMongo;
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            var createdAnimal = await _animalOracle.CreateAnimalAsync(animal);
            await _animalMongo.CreateAnimalAsync(createdAnimal);
            return createdAnimal;
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            var oracleItems = await _animalOracle.GetAllAnimalsAsync();
            var mongoItems = await _animalMongo.GetAllAnimalsAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_animal);
        }

        public Task<Animal> GetAnimalAsync(int id_animal)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _animalOracle.GetAnimalAsync(id_animal),
                () => _animalMongo.GetAnimalAsync(id_animal));
        }

        public async Task<Animal> UpdateAnimalAsync(int id_animal, Animal animal)
        {
            var updatedAnimal = await _animalOracle.UpdateAnimalAsync(id_animal, animal);
            await _animalMongo.UpdateAnimalAsync(id_animal, updatedAnimal);
            return updatedAnimal;
        }

        public async Task<Animal> DeleteAnimalAsync(int id_animal)
        {
            var deletado = await _animalOracle.DeleteAnimalAsync(id_animal);
            await _animalMongo.DeleteAnimalAsync(id_animal);
            return deletado;
        }
    }
}

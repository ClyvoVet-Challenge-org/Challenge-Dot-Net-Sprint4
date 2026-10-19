using challengeFiap.Application.Diagnostics;
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class AnimalService : IAnimalRepository
    {
        private readonly AnimalOracleRepository _animalOracle;
        private readonly AnimalRepository _animalMongo;

        public AnimalService(AnimalOracleRepository animalOracle, AnimalRepository animalMongo)
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
            var animal = await _animalOracle.GetAllAnimalsAsync();

            await _animalMongo.GetAllAnimalsAsync();

            return animal;
        }

        public async Task<Animal> GetAnimalAsync(int id_animal)
        {
            var animal = await _animalOracle.GetAnimalAsync(id_animal);
            await _animalMongo.GetAnimalAsync(id_animal);
            return animal;
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

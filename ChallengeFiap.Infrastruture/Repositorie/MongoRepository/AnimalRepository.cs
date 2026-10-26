using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class AnimalRepository : IAnimalMongoRepository
    {
        private readonly IMongoCollection<Animal> _animalCollection;

        public AnimalRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _animalCollection = context.GetCollection<Animal>(settings.Value.CollectionNames.Animais);
        }

        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            await _animalCollection.InsertOneAsync(animal);
            return animal;
        }
        public async Task<Animal> GetAnimalAsync(int id_animal)
        {
            var animal = await _animalCollection.Find(a => a.Id_animal == id_animal).FirstOrDefaultAsync();
            
            if (animal == null)
            {
                throw new KeyNotFoundException($"Animal com id {id_animal} não encontrado.");
            }

            return animal;
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            return await _animalCollection.Find(a => true).ToListAsync();
        }
        public async Task<Animal> UpdateAnimalAsync(int id_animal, Animal animal)
        {
           var existingAnimal = await _animalCollection.Find(a => a.Id_animal == id_animal).FirstOrDefaultAsync();
            if (existingAnimal == null)
            {
                throw new KeyNotFoundException($"Animal com id {id_animal} não encontrado.");
            }
            animal.Id_animal = existingAnimal.Id_animal;
            await _animalCollection.ReplaceOneAsync(a => a.Id_animal == id_animal, animal);
            return animal;

        }
        public async Task<Animal> DeleteAnimalAsync(int id_animal)
        {
            var animal = await _animalCollection.Find(a => a.Id_animal == id_animal).FirstOrDefaultAsync();
            if (animal == null)
            {
                throw new KeyNotFoundException($"Animal com id {id_animal} não encontrado.");
            }
            await _animalCollection.DeleteOneAsync(a => a.Id_animal == id_animal);
            return animal;
        }


    }
}

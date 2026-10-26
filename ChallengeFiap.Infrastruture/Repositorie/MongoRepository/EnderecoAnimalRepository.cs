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
    public class EnderecoAnimalRepository : IEnderecoAnimalMongoRepository
    {
        private readonly IMongoCollection<EnderecoAnimal> _collection;
        public EnderecoAnimalRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<EnderecoAnimal>(settings.Value.CollectionNames.EnderecoAnimal);
        }

        public async Task<EnderecoAnimal> CreateEnderecoAnimalAsync(EnderecoAnimal enderecoAnimal)
        {
            await _collection.InsertOneAsync(enderecoAnimal);
            return enderecoAnimal;
        }

        public async Task<EnderecoAnimal> GetEnderecoAnimalIdAsync(int id_endereco)
        {
            var endereco = await _collection.Find(e => e.Id_endereco_animal == id_endereco).FirstOrDefaultAsync();
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoAnimal com id {id_endereco} não encontrado.");
            }
            return endereco;
        }

        public async Task<IEnumerable<EnderecoAnimal>> GetAllEnderecoAnimalAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<EnderecoAnimal> UpdateEnderecoAnimalAsync(int id_endereco, EnderecoAnimal enderecoAnimal)
        {
            var existing = await _collection.Find(e => e.Id_endereco_animal == id_endereco).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"EnderecoAnimal com id {id_endereco} não encontrado.");
            }
            enderecoAnimal.Id_endereco_animal = existing.Id_endereco_animal;
            await _collection.ReplaceOneAsync(e => e.Id_endereco_animal == id_endereco, enderecoAnimal);
            return enderecoAnimal;
        }

        public async Task<EnderecoAnimal> DeleteEnderecoAnimalAsync(int id_endereco)
        {
            var existing = await _collection.Find(e => e.Id_endereco_animal == id_endereco).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"EnderecoAnimal com id {id_endereco} não encontrado.");
            }
            await _collection.DeleteOneAsync(e => e.Id_endereco_animal == id_endereco);
            return existing;
        }
    }
}

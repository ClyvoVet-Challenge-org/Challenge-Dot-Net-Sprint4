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
    public class TutorRepository : ITutorMongoRepository
    {
        private readonly IMongoCollection<Tutor> _collection;

        public TutorRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<Tutor>(settings.Value.CollectionNames.Tutor);
        }

        public async Task<Tutor> CreateTutorAsync(Tutor tutor)
        {
            await _collection.InsertOneAsync(tutor);
            return tutor;
        }

        public async Task<Tutor> GetTutorIdAsync(int id_tutor)
        {
            var tutor = await _collection.Find(t => t.Id_tutor == id_tutor).FirstOrDefaultAsync();
            if (tutor == null)
            {
                throw new KeyNotFoundException($"Tutor com id {id_tutor} não encontrado.");
            }
            return tutor;
        }

        public async Task<IEnumerable<Tutor>> GetAllTutorAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Tutor> UpdateTutorAsync(int id_tutor, Tutor tutor)
        {
            var existing = await _collection.Find(t => t.Id_tutor == id_tutor).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Tutor com id {id_tutor} não encontrado.");
            }
            tutor.Id_tutor = existing.Id_tutor;
            await _collection.ReplaceOneAsync(t => t.Id_tutor == id_tutor, tutor);
            return tutor;
        }

        public async Task<Tutor> DeleteTutorAsync(int id_tutor)
        {
            var existing = await _collection.Find(t => t.Id_tutor == id_tutor).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Tutor com id {id_tutor} não encontrado.");
            }
            await _collection.DeleteOneAsync(t => t.Id_tutor == id_tutor);
            return existing;
        }
    }
}

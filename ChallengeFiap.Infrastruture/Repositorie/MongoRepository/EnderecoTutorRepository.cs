using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.MongoRepository;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class EnderecoTutorRepository : IEnderecoTutorMongoRepository
    {
        private readonly IMongoCollection<EnderecoTutor> _collection;
        public EnderecoTutorRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<EnderecoTutor>(settings.Value.CollectionNames.EnderecoTutor);
        }

        public async Task<EnderecoTutor> CreateEnderecoTutorAsync(EnderecoTutor enderecoTutor)
        {
            await _collection.InsertOneAsync(enderecoTutor);
            return enderecoTutor;
        }

        public async Task<EnderecoTutor> GetEnderecoTutorIdAsync(int id_endereco)
        {
            var endereco = await _collection.Find(e => e.Id_endereco_tutor == id_endereco).FirstOrDefaultAsync();
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoTutor com id {id_endereco} não encontrado.");
            }
            return endereco;
        }

        public async Task<IEnumerable<EnderecoTutor>> GetAllEnderecoTutorAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<EnderecoTutor> UpdateEnderecoTutorAsync(int id_endereco, EnderecoTutor enderecoTutor)
        {
            var existing = await _collection.Find(e => e.Id_endereco_tutor == id_endereco).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"EnderecoTutor com id {id_endereco} não encontrado.");
            }
            enderecoTutor.Id_endereco_tutor = existing.Id_endereco_tutor;
            await _collection.ReplaceOneAsync(e => e.Id_endereco_tutor == id_endereco, enderecoTutor);
            return enderecoTutor;
        }

        public async Task<EnderecoTutor> DeleteEnderecoTutorAsync(int id_endereco)
        {
            var existing = await _collection.Find(e => e.Id_endereco_tutor == id_endereco).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"EnderecoTutor com id {id_endereco} não encontrado.");
            }
            await _collection.DeleteOneAsync(e => e.Id_endereco_tutor == id_endereco);
            return existing;
        }
    }
}

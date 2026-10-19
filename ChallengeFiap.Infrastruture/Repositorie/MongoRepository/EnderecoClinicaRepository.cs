using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class EnderecoClinicaRepository : IenderecoClinicaRepository
    {
        private readonly IMongoCollection<EnderecoClinica> _collection;
        public EnderecoClinicaRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<EnderecoClinica>(settings.Value.CollectionNames.EnderecoClinica);
        }

        public async Task<EnderecoClinica> CreateEnderecoClinicaAsync(EnderecoClinica enderecoClinica)
        {
            await _collection.InsertOneAsync(enderecoClinica);
            return enderecoClinica;
        }

        public async Task<EnderecoClinica> GetEnderecoClinicaIdAsync(int id_endereco_clinica)
        {
            var endereco = await _collection.Find(e => e.Id_endereco_clinica == id_endereco_clinica).FirstOrDefaultAsync();
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoClinica com id {id_endereco_clinica} não encontrado.");
            }
            return endereco;
        }

        public async Task<IEnumerable<EnderecoClinica>> GetAllEnderecoClinicaAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<EnderecoClinica> UpdateEnderecoClinicaAsync(int id_endereco_clinica, EnderecoClinica enderecoClinica)
        {
            var existing = await _collection.Find(e => e.Id_endereco_clinica == id_endereco_clinica).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"EnderecoClinica com id {id_endereco_clinica} não encontrado.");
            }
            enderecoClinica.Id_endereco_clinica = existing.Id_endereco_clinica;
            await _collection.ReplaceOneAsync(e => e.Id_endereco_clinica == id_endereco_clinica, enderecoClinica);
            return enderecoClinica;
        }

        public async Task<EnderecoClinica> DeleteEnderecoClinicaAsync(int id_endereco_clinica)
        {
            var existing = await _collection.Find(e => e.Id_endereco_clinica == id_endereco_clinica).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"EnderecoClinica com id {id_endereco_clinica} não encontrado.");
            }
            await _collection.DeleteOneAsync(e => e.Id_endereco_clinica == id_endereco_clinica);
            return existing;
        }
    }
}

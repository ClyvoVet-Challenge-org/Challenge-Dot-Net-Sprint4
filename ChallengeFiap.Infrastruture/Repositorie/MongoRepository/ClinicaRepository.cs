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
    public class ClinicaRepository : IClinicaMongoRepository
    {
        private readonly IMongoCollection<Clinica> _collection;

        public ClinicaRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<Clinica>(settings.Value.CollectionNames.Clinicas);
        }

        public async Task<Clinica> CreateadAsync(Clinica clinica)
        {
            await _collection.InsertOneAsync(clinica);
            return clinica;
        }

        public async Task<Clinica> GetClinicaIdAsync(int id_Clinica)
        {
            var clinica = await _collection.Find(c => c.Id_clinica == id_Clinica).FirstOrDefaultAsync();
            if (clinica == null)
            {
                throw new KeyNotFoundException($"Clinica com id {id_Clinica} não encontrada.");
            }
            return clinica;
        }

        public async Task<IEnumerable<Clinica>> GetAllClinicaAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Clinica> UpdateClinicaAsync(int id_clinica, Clinica clinica)
        {
            var existing = await _collection.Find(c => c.Id_clinica == id_clinica).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Clinica com id {id_clinica} não encontrada.");
            }
            clinica.Id_clinica = existing.Id_clinica;
            await _collection.ReplaceOneAsync(c => c.Id_clinica == id_clinica, clinica);
            return clinica;
        }

        public async Task<Clinica> DeleteClinicaAsync(int id_Clinica)
        {
            var existing = await _collection.Find(c => c.Id_clinica == id_Clinica).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Clinica com id {id_Clinica} não encontrada.");
            }
            await _collection.DeleteOneAsync(c => c.Id_clinica == id_Clinica);
            return existing;
        }
    }
}

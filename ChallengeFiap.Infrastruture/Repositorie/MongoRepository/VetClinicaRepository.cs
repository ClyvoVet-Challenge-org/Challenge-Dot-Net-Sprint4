using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.MongoRepository;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class VetClinicaRepository : IVetClinicaMongoRepository
    {
        private readonly IMongoCollection<VetClinica> _collection;

        public VetClinicaRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<VetClinica>(settings.Value.CollectionNames.VetClinicas);
        }

        public async Task<VetClinica> CreateVetClinicaAsync(VetClinica VetClinica)
        {
            await _collection.InsertOneAsync(VetClinica);
            return VetClinica;
        }

        public async Task<VetClinica> GetVetClinicaIdAsync(int id_VetClinica)
        {
            var item = await _collection.Find(v => v.Id_clinica_vet == id_VetClinica).FirstOrDefaultAsync();
            if (item == null)
            {
                throw new KeyNotFoundException($"VetClinica com id {id_VetClinica} não encontrada.");
            }
            return item;
        }

        public async Task<IEnumerable<VetClinica>> GetAllVetClinicaAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<VetClinica> UpdateVetClinicaAsync(int id_VetClinica, VetClinica VetClinica)
        {
            var existing = await _collection.Find(v => v.Id_clinica_vet == id_VetClinica).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"VetClinica com id {id_VetClinica} não encontrada.");
            }
            VetClinica.Id_clinica_vet = existing.Id_clinica_vet;
            await _collection.ReplaceOneAsync(v => v.Id_clinica_vet == id_VetClinica, VetClinica);
            return VetClinica;
        }

        public async Task<VetClinica> DeleteVetClinicaAsync(int id_VetClinica)
        {
            var existing = await _collection.Find(v => v.Id_clinica_vet == id_VetClinica).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"VetClinica com id {id_VetClinica} não encontrada.");
            }
            await _collection.DeleteOneAsync(v => v.Id_clinica_vet == id_VetClinica);
            return existing;
        }
    }
}

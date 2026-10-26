using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class VeterinarioRepository : IVeterinarioMongoRepository
    {
        private readonly IMongoCollection<Veterinario> _collection;

        public VeterinarioRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<Veterinario>(settings.Value.CollectionNames.Veterinarios);
        }

        public async Task<Veterinario> CreateVeterinarioAsync(Veterinario veterinario)
        {
            await _collection.InsertOneAsync(veterinario);
            return veterinario;
        }

        public async Task<Veterinario> GetVeterinarioIdAsync(int id_veterinario)
        {
            var item = await _collection.Find(v => v.Id_vet == id_veterinario).FirstOrDefaultAsync();
            if (item == null)
            {
                throw new KeyNotFoundException($"Veterinario com id {id_veterinario} não encontrado.");
            }
            return item;
        }

        public async Task<IEnumerable<Veterinario>> GetAllVeterinarioAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Veterinario> UpdateVeterinarioAsync(int id_veterinario, Veterinario veterinario)
        {
            var existing = await _collection.Find(v => v.Id_vet == id_veterinario).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Veterinario com id {id_veterinario} não encontrado.");
            }
            veterinario.Id_vet = existing.Id_vet;
            await _collection.ReplaceOneAsync(v => v.Id_vet == id_veterinario, veterinario);
            return veterinario;
        }

        public async Task<Veterinario> DeleteVeterinarioAsync(int id_veterinario)
        {
            var existing = await _collection.Find(v => v.Id_vet == id_veterinario).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Veterinario com id {id_veterinario} não encontrado.");
            }
            await _collection.DeleteOneAsync(v => v.Id_vet == id_veterinario);
            return existing;
        }
    }
}

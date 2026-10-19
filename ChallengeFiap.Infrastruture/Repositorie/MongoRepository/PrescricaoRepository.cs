using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class PrescricaoRepository : IprescricaoRepository
    {
        private readonly IMongoCollection<Prescricao> _collection;
        public PrescricaoRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<Prescricao>(settings.Value.CollectionNames.Prescricoes);
        }

        public async Task<Prescricao> CreatePrescricaoAsync(Prescricao prescricao)
        {
            await _collection.InsertOneAsync(prescricao);
            return prescricao;
        }

        public async Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao)
        {
            var prescricao = await _collection.Find(p => p.Id_prescricao == id_prescricao).FirstOrDefaultAsync();
            if (prescricao == null)
            {
                throw new KeyNotFoundException($"Prescricao com id {id_prescricao} não encontrada.");
            }
            return prescricao;
        }

        public async Task<IEnumerable<Prescricao>> GetAllPrescricaoAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Prescricao> UpdatePrescricaoAsync(int id_prescricao, Prescricao prescricao)
        {
            var existing = await _collection.Find(p => p.Id_prescricao == id_prescricao).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Prescricao com id {id_prescricao} não encontrada.");
            }
            prescricao.Id_prescricao = existing.Id_prescricao;
            await _collection.ReplaceOneAsync(p => p.Id_prescricao == id_prescricao, prescricao);
            return prescricao;
        }

        public async Task<Prescricao> DeletePrescricaoAsync(int id_prescricao)
        {
            var existing = await _collection.Find(p => p.Id_prescricao == id_prescricao).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Prescricao com id {id_prescricao} não encontrada.");
            }
            await _collection.DeleteOneAsync(p => p.Id_prescricao == id_prescricao);
            return existing;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class MedicamentoRepository : IMedicamentoRepository
    {
        private readonly IMongoCollection<Medicamento> _collection;
        public MedicamentoRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<Medicamento>(settings.Value.CollectionNames.Medicamentos);
        }

        public async Task<Medicamento> CreateMedicamentoAsync(Medicamento medicamento)
        {
            await _collection.InsertOneAsync(medicamento);
            return medicamento;
        }

        public async Task<Medicamento> GetMedicamentoIdAsync(int id_medicamento)
        {
            var medicamento = await _collection.Find(m => m.Id_medicamento == id_medicamento).FirstOrDefaultAsync();
            if (medicamento == null)
            {
                throw new KeyNotFoundException($"Medicamento com id {id_medicamento} não encontrado.");
            }
            return medicamento;
        }

        public async Task<IEnumerable<Medicamento>> GetAllMedicamentoAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Medicamento> UpdateMedicamentoAsync(int id_medicamento, Medicamento medicamento)
        {
            var existing = await _collection.Find(m => m.Id_medicamento == id_medicamento).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Medicamento com id {id_medicamento} não encontrado.");
            }
            medicamento.Id_medicamento = existing.Id_medicamento;
            await _collection.ReplaceOneAsync(m => m.Id_medicamento == id_medicamento, medicamento);
            return medicamento;
        }

        public async Task<Medicamento> DeleteMedicamentoAsync(int id_medicamento)
        {
            var existing = await _collection.Find(m => m.Id_medicamento == id_medicamento).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Medicamento com id {id_medicamento} não encontrado.");
            }
            await _collection.DeleteOneAsync(m => m.Id_medicamento == id_medicamento);
            return existing;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using ChallengeFiap.Infrastruture.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.MongoRepository
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly IMongoCollection<Consulta> _collection;

        public ConsultaRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<Consulta>(settings.Value.CollectionNames.Consultas);
        }

        public async Task<Consulta> CreateConsultaAsync(Consulta consulta)
        {
            await _collection.InsertOneAsync(consulta);
            return consulta;
        }

        public async Task<Consulta> GetConsultaIdAsync(int id_consulta)
        {
            var consulta = await _collection.Find(c => c.Id_consulta == id_consulta).FirstOrDefaultAsync();
            if (consulta == null)
            {
                throw new KeyNotFoundException($"Consulta com id {id_consulta} não encontrada.");
            }
            return consulta;
        }

        public async Task<IEnumerable<Consulta>> GetAllConsultaAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Consulta> UpdateConsultaAsync(int id_consulta, Consulta consulta)
        {
            var existing = await _collection.Find(c => c.Id_consulta == id_consulta).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Consulta com id {id_consulta} não encontrada.");
            }
            consulta.Id_consulta = existing.Id_consulta;
            await _collection.ReplaceOneAsync(c => c.Id_consulta == id_consulta, consulta);
            return consulta;
        }

        public async Task<Consulta> DeleteConsultaAsync(int id_consulta)
        {
            var existing = await _collection.Find(c => c.Id_consulta == id_consulta).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"Consulta com id {id_consulta} não encontrada.");
            }
            await _collection.DeleteOneAsync(c => c.Id_consulta == id_consulta);
            return existing;
        }
    }
}

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
    public class CarteiraVacinalRepository : ICarteiraVacinalMongoRepository
    {
        private readonly IMongoCollection<CarteiraVacinal> _collection;
        public CarteiraVacinalRepository(MongoDbContext context, IOptions<MongoStoreDataBaseSettings> settings)
        {
            _collection = context.GetCollection<CarteiraVacinal>(settings.Value.CollectionNames.CarteirasVacinais);
        }

        public async Task<CarteiraVacinal> CreateCarteiraVacinalAsync(CarteiraVacinal carteiraVacinal)
        {
            await _collection.InsertOneAsync(carteiraVacinal);
            return carteiraVacinal;
        }

        public async Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal)
        {
            var carteira = await _collection.Find(c => c.Id_carteiraVacinal == id_CarteiraVacinal).FirstOrDefaultAsync();
            if (carteira == null)
            {
                throw new KeyNotFoundException($"CarteiraVacinal com id {id_CarteiraVacinal} não encontrada.");
            }
            return carteira;
        }

        public async Task<IEnumerable<CarteiraVacinal>> GetAllCarteiraVacinalAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<CarteiraVacinal> UpdateCarteiraVacinalAsync(int id_carteira, CarteiraVacinal carteiraVacinal)
        {
            var existing = await _collection.Find(c => c.Id_carteiraVacinal == id_carteira).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"CarteiraVacinal com id {id_carteira} não encontrada.");
            }
            carteiraVacinal.Id_carteiraVacinal = existing.Id_carteiraVacinal;
            await _collection.ReplaceOneAsync(c => c.Id_carteiraVacinal == id_carteira, carteiraVacinal);
            return carteiraVacinal;
        }

        public async Task<CarteiraVacinal> DeleteCarteiraVacinalAsync(int id_CarteiraVacinal)
        {
            var existing = await _collection.Find(c => c.Id_carteiraVacinal == id_CarteiraVacinal).FirstOrDefaultAsync();
            if (existing == null)
            {
                throw new KeyNotFoundException($"CarteiraVacinal com id {id_CarteiraVacinal} não encontrada.");
            }
            await _collection.DeleteOneAsync(c => c.Id_carteiraVacinal == id_CarteiraVacinal);
            return existing;
        }
    }
}

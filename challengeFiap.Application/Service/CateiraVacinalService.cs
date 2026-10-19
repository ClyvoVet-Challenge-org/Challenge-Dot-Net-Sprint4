using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class CateiraVacinalService : ICarteiraVacinalRepository
    {
        private readonly CarteiraVacinalOracleRepository _oracle;
        private readonly CarteiraVacinalRepository _mongo;

        public CateiraVacinalService(CarteiraVacinalOracleRepository oracle, CarteiraVacinalRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<CarteiraVacinal> CreateCarteiraVacinalAsync(CarteiraVacinal carteiraVacinal)
        {
            var created = await _oracle.CreateCarteiraVacinalAsync(carteiraVacinal);
            await _mongo.CreateCarteiraVacinalAsync(created);
            return created;
        }

        public async Task<IEnumerable<CarteiraVacinal>> GetAllCarteiraVacinalAsync()
        {
            var c_v = await _oracle.GetAllCarteiraVacinalAsync();
            await _mongo.GetAllCarteiraVacinalAsync();
            return c_v;
        }

        public async Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal)
        {
            var carteiraVacinal = await _oracle.GetCarteiraVacinalIdAsync(id_CarteiraVacinal);
            await _mongo.GetCarteiraVacinalIdAsync(id_CarteiraVacinal);
            return carteiraVacinal;
        }

        public async Task<CarteiraVacinal> UpdateCarteiraVacinalAsync(int id_carteira, CarteiraVacinal carteiraVacinal)
        {
            var updateCateiraVacinal = await _oracle.UpdateCarteiraVacinalAsync(id_carteira, carteiraVacinal);
            await _mongo.UpdateCarteiraVacinalAsync(id_carteira, updateCateiraVacinal);
            return updateCateiraVacinal;
        }

        public async Task<CarteiraVacinal> DeleteCarteiraVacinalAsync(int id_CarteiraVacinal)
        {
            var deletedo = await _oracle.DeleteCarteiraVacinalAsync(id_CarteiraVacinal);
            await _mongo.DeleteCarteiraVacinalAsync(id_CarteiraVacinal);
            return deletedo;
        }
    }
}


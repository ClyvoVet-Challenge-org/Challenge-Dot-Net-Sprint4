using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class CateiraVacinalService : ICarteiraVacinalService
    {
        private readonly ICarteiraVacinalOracleRepository _oracle;
        private readonly ICarteiraVacinalMongoRepository _mongo;

        public CateiraVacinalService(ICarteiraVacinalOracleRepository oracle, ICarteiraVacinalMongoRepository mongo)
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
            var oracleItems = await _oracle.GetAllCarteiraVacinalAsync();
            var mongoItems = await _mongo.GetAllCarteiraVacinalAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_carteiraVacinal);
        }

        public Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetCarteiraVacinalIdAsync(id_CarteiraVacinal),
                () => _mongo.GetCarteiraVacinalIdAsync(id_CarteiraVacinal));
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

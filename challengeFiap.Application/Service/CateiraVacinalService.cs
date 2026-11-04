using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;

using challengeFiap.Domain.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace challengeFiap.Application.Service
{
    public class CateiraVacinalService : ICarteiraVacinalService
    {
        private readonly ICarteiraVacinalOracleRepository _oracle;
        private readonly ICarteiraVacinalMongoRepository _mongo;
        private readonly ILogger<CateiraVacinalService> _logger;

        public CateiraVacinalService(
            ICarteiraVacinalOracleRepository oracle,
            ICarteiraVacinalMongoRepository mongo,
            ILogger<CateiraVacinalService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<CarteiraVacinal> CreateCarteiraVacinalAsync(CarteiraVacinal carteiraVacinal)
        {
            _logger.LogInformation("Criando carteira vacinal.");
            var created = await _oracle.CreateCarteiraVacinalAsync(carteiraVacinal);
            await _mongo.CreateCarteiraVacinalAsync(created);
            return created;
        }

        public async Task<IEnumerable<CarteiraVacinal>> GetAllCarteiraVacinalAsync()
        {
            _logger.LogInformation("Buscando todas as carteiras vacinais.");
            var oracleItems = await _oracle.GetAllCarteiraVacinalAsync();
            var mongoItems = await _mongo.GetAllCarteiraVacinalAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_carteiraVacinal);
        }

        public Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal)
        {
            _logger.LogInformation("Buscando carteira vacinal por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetCarteiraVacinalIdAsync(id_CarteiraVacinal),
                () => _mongo.GetCarteiraVacinalIdAsync(id_CarteiraVacinal));
        }

        public async Task<CarteiraVacinal> UpdateCarteiraVacinalAsync(int id_carteira, CarteiraVacinal carteiraVacinal)
        {
            _logger.LogInformation("Atualizando carteira vacinal.");
            var updateCateiraVacinal = await _oracle.UpdateCarteiraVacinalAsync(id_carteira, carteiraVacinal);
            await _mongo.UpdateCarteiraVacinalAsync(id_carteira, updateCateiraVacinal);
            return updateCateiraVacinal;
        }

        public async Task<CarteiraVacinal> DeleteCarteiraVacinalAsync(int id_CarteiraVacinal)
        {
            _logger.LogInformation("Excluindo carteira vacinal.");
            var deletedo = await _oracle.DeleteCarteiraVacinalAsync(id_CarteiraVacinal);
            await _mongo.DeleteCarteiraVacinalAsync(id_CarteiraVacinal);
            return deletedo;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace challengeFiap.Application.Service
{
    public class TutorService : ITutorService
    {
        private readonly ITutorOracleRepository _oracle;
        private readonly ITutorMongoRepository _mongo;
        private readonly ILogger<TutorService> _logger;

        public TutorService(
            ITutorOracleRepository oracle,
            ITutorMongoRepository mongo,
            ILogger<TutorService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<Tutor> CreateTutorAsync(Tutor tutor)
        {
            _logger.LogInformation("Criando tutor.");
            var created = await _oracle.CreateTutorAsync(tutor);
            await _mongo.CreateTutorAsync(created);
            return created;
        }

        public async Task<IEnumerable<Tutor>> GetAllTutorAsync()
        {
            _logger.LogInformation("Buscando todos os tutores.");
            var oracleItems = await _oracle.GetAllTutorAsync();
            var mongoItems = await _mongo.GetAllTutorAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_tutor);
        }

        public Task<Tutor> GetTutorIdAsync(int id_tutor)
        {
            _logger.LogInformation("Buscando tutor por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetTutorIdAsync(id_tutor),
                () => _mongo.GetTutorIdAsync(id_tutor));
        }

        public async Task<Tutor> UpdateTutorAsync(int id_tutor, Tutor tutor)
        {
            _logger.LogInformation("Atualizando tutor.");
            var updated = await _oracle.UpdateTutorAsync(id_tutor, tutor);
            await _mongo.UpdateTutorAsync(id_tutor, updated);
            return updated;
        }

        public async Task<Tutor> DeleteTutorAsync(int id_tutor)
        {
            _logger.LogInformation("Excluindo tutor.");
            var deleted = await _oracle.DeleteTutorAsync(id_tutor);
            await _mongo.DeleteTutorAsync(id_tutor);
            return deleted;
        }
    }
}

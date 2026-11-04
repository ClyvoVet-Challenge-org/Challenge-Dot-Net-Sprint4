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
    public class PrescricaoService : IPrescricaoService
    {
        private readonly IPrescricaoOracleRepository _oracle;
        private readonly IPrescricaoMongoRepository _mongo;
        private readonly ILogger<PrescricaoService> _logger;

        public PrescricaoService(
            IPrescricaoOracleRepository oracle,
            IPrescricaoMongoRepository mongo,
            ILogger<PrescricaoService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<Prescricao> CreatePrescricaoAsync(Prescricao prescricao)
        {
            _logger.LogInformation("Criando prescrição.");
            var created = await _oracle.CreatePrescricaoAsync(prescricao);
            await _mongo.CreatePrescricaoAsync(created);
            return created;
        }

        public async Task<IEnumerable<Prescricao>> GetAllPrescricaoAsync()
        {
            _logger.LogInformation("Buscando todas as prescrições.");
            var oracleItems = await _oracle.GetAllPrescricaoAsync();
            var mongoItems = await _mongo.GetAllPrescricaoAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_prescricao);
        }

        public Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao)
        {
            _logger.LogInformation("Buscando prescrição por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetPrescricaoIdAsync(id_prescricao),
                () => _mongo.GetPrescricaoIdAsync(id_prescricao));
        }

        public async Task<Prescricao> UpdatePrescricaoAsync(int id_prescricao, Prescricao prescricao)
        {
            _logger.LogInformation("Atualizando prescrição.");
            var updated = await _oracle.UpdatePrescricaoAsync(id_prescricao, prescricao);
            await _mongo.UpdatePrescricaoAsync(id_prescricao, updated);
            return updated;
        }

        public async Task<Prescricao> DeletePrescricaoAsync(int id_prescricao)
        {
            _logger.LogInformation("Excluindo prescrição.");
            var deleted = await _oracle.DeletePrescricaoAsync(id_prescricao);
            await _mongo.DeletePrescricaoAsync(id_prescricao);
            return deleted;
        }
    }
}

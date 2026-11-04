using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using Microsoft.Extensions.Logging;


namespace challengeFiap.Application.Service
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoOracleRepository _oracle;
        private readonly IMedicamentoMongoRepository _mongo;
        private readonly ILogger<MedicamentoService> _logger;

        public MedicamentoService(
            IMedicamentoOracleRepository oracle,
            IMedicamentoMongoRepository mongo,
            ILogger<MedicamentoService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<Medicamento> CreateMedicamentoAsync(Medicamento medicamento)
        {
            _logger.LogInformation("Criando medicamento.");
            var created = await _oracle.CreateMedicamentoAsync(medicamento);
            await _mongo.CreateMedicamentoAsync(created);
            return created;
        }

        public async Task<IEnumerable<Medicamento>> GetAllMedicamentoAsync()
        {
            _logger.LogInformation("Buscando todos os medicamentos.");
            var oracleItems = await _oracle.GetAllMedicamentoAsync();
            var mongoItems = await _mongo.GetAllMedicamentoAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_medicamento);
        }

        public Task<Medicamento> GetMedicamentoIdAsync(int id_medicamento)
        {
            _logger.LogInformation("Buscando medicamento por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetMedicamentoIdAsync(id_medicamento),
                () => _mongo.GetMedicamentoIdAsync(id_medicamento));
        }

        public async Task<Medicamento> UpdateMedicamentoAsync(int id_medicamento, Medicamento medicamento)
        {
            _logger.LogInformation("Atualizando medicamento.");
            var updated = await _oracle.UpdateMedicamentoAsync(id_medicamento, medicamento);
            await _mongo.UpdateMedicamentoAsync(id_medicamento, updated);
            return updated;
        }

        public async Task<Medicamento> DeleteMedicamentoAsync(int id_medicamento)
        {
            _logger.LogInformation("Excluindo medicamento.");
            var deleted = await _oracle.DeleteMedicamentoAsync(id_medicamento);
            await _mongo.DeleteMedicamentoAsync(id_medicamento);
            return deleted;
        }
    }
}

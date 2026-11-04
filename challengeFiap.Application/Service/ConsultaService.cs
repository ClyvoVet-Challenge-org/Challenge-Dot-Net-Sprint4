using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using Microsoft.Extensions.Logging;


namespace challengeFiap.Application.Service
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaOracleRepository _oracle;
        private readonly IConsultaMongoRepository _mongo;
        private readonly ILogger<ConsultaService> _logger;

        public ConsultaService(
            IConsultaOracleRepository oracle,
            IConsultaMongoRepository mongo,
            ILogger<ConsultaService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<Consulta> CreateConsultaAsync(Consulta consulta)
        {
            _logger.LogInformation("Criando consulta.");
            var created = await _oracle.CreateConsultaAsync(consulta);
            await _mongo.CreateConsultaAsync(created);
            return created;
        }

        public async Task<IEnumerable<Consulta>> GetAllConsultaAsync()
        {
            _logger.LogInformation("Buscando todas as consultas.");
            var oracleItems = await _oracle.GetAllConsultaAsync();
            var mongoItems = await _mongo.GetAllConsultaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_consulta);
        }

        public Task<Consulta> GetConsultaIdAsync(int id_consulta)
        {
            _logger.LogInformation("Buscando consulta por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetConsultaIdAsync(id_consulta),
                () => _mongo.GetConsultaIdAsync(id_consulta));
        }

        public async Task<Consulta> UpdateConsultaAsync(int id_consulta, Consulta consulta)
        {
            _logger.LogInformation("Atualizando consulta.");
            var updated = await _oracle.UpdateConsultaAsync(id_consulta, consulta);
            await _mongo.UpdateConsultaAsync(id_consulta, updated);
            return updated;
        }

        public async Task<Consulta> DeleteConsultaAsync(int id_consulta)
        {
            _logger.LogInformation("Excluindo consulta.");
            var deleted = await _oracle.DeleteConsultaAsync(id_consulta);
            await _mongo.DeleteConsultaAsync(id_consulta);
            return deleted;
        }
    }
}

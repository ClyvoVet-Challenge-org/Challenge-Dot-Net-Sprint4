
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using Microsoft.Extensions.Logging;


namespace challengeFiap.Application.Service
{
    public class EnderecoTutorService : IEnderecoTutorService
    {
        private readonly IEnderecoTutorOracleRepository _oracle;
        private readonly IEnderecoTutorMongoRepository _mongo;
        private readonly ILogger<EnderecoTutorService> _logger;

        public EnderecoTutorService(
            IEnderecoTutorOracleRepository oracle,
            IEnderecoTutorMongoRepository mongo,
            ILogger<EnderecoTutorService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<EnderecoTutor> CreateEnderecoTutorAsync(EnderecoTutor enderecoTutor)
        {
            _logger.LogInformation("Criando endereço de tutor.");
            var created = await _oracle.CreateEnderecoTutorAsync(enderecoTutor);
            await _mongo.CreateEnderecoTutorAsync(created);
            return created;
        }

        public async Task<IEnumerable<EnderecoTutor>> GetAllEnderecoTutorAsync()
        {
            _logger.LogInformation("Buscando todos os endereços de tutores.");
            var oracleItems = await _oracle.GetAllEnderecoTutorAsync();
            var mongoItems = await _mongo.GetAllEnderecoTutorAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_endereco_tutor);
        }

        public Task<EnderecoTutor> GetEnderecoTutorIdAsync(int id_endereco)
        {
            _logger.LogInformation("Buscando endereço de tutor por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetEnderecoTutorIdAsync(id_endereco),
                () => _mongo.GetEnderecoTutorIdAsync(id_endereco));
        }

        public async Task<EnderecoTutor> UpdateEnderecoTutorAsync(int id_endereco, EnderecoTutor enderecoTutor)
        {
            _logger.LogInformation("Atualizando endereço de tutor.");
            var updated = await _oracle.UpdateEnderecoTutorAsync(id_endereco, enderecoTutor);
            await _mongo.UpdateEnderecoTutorAsync(id_endereco, updated);
            return updated;
        }

        public async Task<EnderecoTutor> DeleteEnderecoTutorAsync(int id_endereco)
        {
            _logger.LogInformation("Excluindo endereço de tutor.");
            var deleted = await _oracle.DeleteEnderecoTutorAsync(id_endereco);
            await _mongo.DeleteEnderecoTutorAsync(id_endereco);
            return deleted;
        }
    }
}

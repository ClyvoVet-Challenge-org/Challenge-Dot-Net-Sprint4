using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using Microsoft.Extensions.Logging;


namespace challengeFiap.Application.Service
{
    public class EnderecoAnimalService : IEnderecoAnimalService
    {
        private readonly IEnderecoAnimalOracleRepository _oracle;
        private readonly IEnderecoAnimalMongoRepository _mongo;
        private readonly ILogger<EnderecoAnimalService> _logger;

        public EnderecoAnimalService(
            IEnderecoAnimalOracleRepository oracle,
            IEnderecoAnimalMongoRepository mongo,
            ILogger<EnderecoAnimalService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<EnderecoAnimal> CreateEnderecoAnimalAsync(EnderecoAnimal enderecoAnimal)
        {
            _logger.LogInformation("Criando endereço de animal.");
            var created = await _oracle.CreateEnderecoAnimalAsync(enderecoAnimal);
            await _mongo.CreateEnderecoAnimalAsync(created);
            return created;
        }

        public async Task<IEnumerable<EnderecoAnimal>> GetAllEnderecoAnimalAsync()
        {
            _logger.LogInformation("Buscando todos os endereços de animais.");
            var oracleItems = await _oracle.GetAllEnderecoAnimalAsync();
            var mongoItems = await _mongo.GetAllEnderecoAnimalAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_endereco_animal);
        }

        public Task<EnderecoAnimal> GetEnderecoAnimalIdAsync(int id_endereco)
        {
            _logger.LogInformation("Buscando endereço de animal por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetEnderecoAnimalIdAsync(id_endereco),
                () => _mongo.GetEnderecoAnimalIdAsync(id_endereco));
        }

        public async Task<EnderecoAnimal> UpdateEnderecoAnimalAsync(int id_endereco, EnderecoAnimal enderecoAnimal)
        {
            _logger.LogInformation("Atualizando endereço de animal.");
            var updated = await _oracle.UpdateEnderecoAnimalAsync(id_endereco, enderecoAnimal);
            await _mongo.UpdateEnderecoAnimalAsync(id_endereco, updated);
            return updated;
        }

        public async Task<EnderecoAnimal> DeleteEnderecoAnimalAsync(int id_endereco)
        {
            _logger.LogInformation("Excluindo endereço de animal.");
            var deleted = await _oracle.DeleteEnderecoAnimalAsync(id_endereco);
            await _mongo.DeleteEnderecoAnimalAsync(id_endereco);
            return deleted;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using Microsoft.Extensions.Logging;

namespace challengeFiap.Application.Service
{
    public class VeterinariosService : IVeterinarioService
    {
        private readonly IVeterinarioOracleRepository _oracle;
        private readonly IVeterinarioMongoRepository _mongo;
        private readonly ILogger<VeterinariosService> _logger;

        public VeterinariosService(
            IVeterinarioOracleRepository oracle,
            IVeterinarioMongoRepository mongo,
            ILogger<VeterinariosService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<Veterinario> CreateVeterinarioAsync(Veterinario veterinario)
        {
            _logger.LogInformation("Criando veterinário.");
            var created = await _oracle.CreateVeterinarioAsync(veterinario);
            await _mongo.CreateVeterinarioAsync(created);
            return created;
        }

        public async Task<IEnumerable<Veterinario>> GetAllVeterinarioAsync()
        {
            _logger.LogInformation("Buscando todos os veterinários.");
            var oracleItems = await _oracle.GetAllVeterinarioAsync();
            var mongoItems = await _mongo.GetAllVeterinarioAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_vet);
        }

        public Task<Veterinario> GetVeterinarioIdAsync(int id_veterinario)
        {
            _logger.LogInformation("Buscando veterinário por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetVeterinarioIdAsync(id_veterinario),
                () => _mongo.GetVeterinarioIdAsync(id_veterinario));
        }

        public async Task<Veterinario> UpdateVeterinarioAsync(int id_veterinario, Veterinario veterinario)
        {
            _logger.LogInformation("Atualizando veterinário.");
            var updated = await _oracle.UpdateVeterinarioAsync(id_veterinario, veterinario);
            await _mongo.UpdateVeterinarioAsync(id_veterinario, updated);
            return updated;
        }

        public async Task<Veterinario> DeleteVeterinarioAsync(int id_veterinario)
        {
            _logger.LogInformation("Excluindo veterinário.");
            var deleted = await _oracle.DeleteVeterinarioAsync(id_veterinario);
            await _mongo.DeleteVeterinarioAsync(id_veterinario);
            return deleted;
        }
    }
}

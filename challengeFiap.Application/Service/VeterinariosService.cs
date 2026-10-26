using challengeFiap.Domain.Entities;
using challengeFiap.Application.InterfacesService;
using challengeFiap.Infrastruture.Interfaces.MongoRepository;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;

namespace challengeFiap.Application.Service
{
    public class VeterinariosService : IVeterinarioService
    {
        private readonly IVeterinarioOracleRepository _oracle;
        private readonly IVeterinarioMongoRepository _mongo;

        public VeterinariosService(IVeterinarioOracleRepository oracle, IVeterinarioMongoRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<Veterinario> CreateVeterinarioAsync(Veterinario veterinario)
        {
            var created = await _oracle.CreateVeterinarioAsync(veterinario);
            await _mongo.CreateVeterinarioAsync(created);
            return created;
        }

        public async Task<IEnumerable<Veterinario>> GetAllVeterinarioAsync()
        {
            var oracleItems = await _oracle.GetAllVeterinarioAsync();
            var mongoItems = await _mongo.GetAllVeterinarioAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_vet);
        }

        public Task<Veterinario> GetVeterinarioIdAsync(int id_veterinario)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetVeterinarioIdAsync(id_veterinario),
                () => _mongo.GetVeterinarioIdAsync(id_veterinario));
        }

        public async Task<Veterinario> UpdateVeterinarioAsync(int id_veterinario, Veterinario veterinario)
        {
            var updated = await _oracle.UpdateVeterinarioAsync(id_veterinario, veterinario);
            await _mongo.UpdateVeterinarioAsync(id_veterinario, updated);
            return updated;
        }

        public async Task<Veterinario> DeleteVeterinarioAsync(int id_veterinario)
        {
            var deleted = await _oracle.DeleteVeterinarioAsync(id_veterinario);
            await _mongo.DeleteVeterinarioAsync(id_veterinario);
            return deleted;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class VeterinariosService : IVeterinarioRepository
    {
        private readonly VeterinarioOracleRepository _oracle;
        private readonly VeterinarioRepository _mongo;

        public VeterinariosService(VeterinarioOracleRepository oracle, VeterinarioRepository mongo)
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
            var list = await _oracle.GetAllVeterinarioAsync();
            await _mongo.GetAllVeterinarioAsync();
            return list;
        }

        public async Task<Veterinario> GetVeterinarioIdAsync(int id_veterinario)
        {
            var item = await _oracle.GetVeterinarioIdAsync(id_veterinario);
            await _mongo.GetVeterinarioIdAsync(id_veterinario);
            return item;
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

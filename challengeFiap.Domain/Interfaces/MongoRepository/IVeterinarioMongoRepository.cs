using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.MongoRepository;

public interface IVeterinarioMongoRepository
{
    Task<Veterinario> CreateVeterinarioAsync(Veterinario veterinario);
    Task<Veterinario> UpdateVeterinarioAsync(int id_veterinario, Veterinario veterinario);
    Task<Veterinario> GetVeterinarioIdAsync(int id_veterinario);
    Task<Veterinario> DeleteVeterinarioAsync(int id_veterinario);
    Task<IEnumerable<Veterinario>> GetAllVeterinarioAsync();
}

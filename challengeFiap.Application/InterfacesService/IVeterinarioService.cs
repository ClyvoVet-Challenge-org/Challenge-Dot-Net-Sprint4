using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.InterfacesService;

public interface IVeterinarioService
{
    Task<Veterinario> CreateVeterinarioAsync(Veterinario veterinario);
    Task<Veterinario> UpdateVeterinarioAsync(int id_veterinario, Veterinario veterinario);
    Task<Veterinario> GetVeterinarioIdAsync(int id_veterinario);
    Task<Veterinario> DeleteVeterinarioAsync(int id_veterinario);
    Task<IEnumerable<Veterinario>> GetAllVeterinarioAsync();
}

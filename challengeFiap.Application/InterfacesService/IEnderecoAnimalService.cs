using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.InterfacesService;

public interface IEnderecoAnimalService
{
    Task<EnderecoAnimal> CreateEnderecoAnimalAsync(EnderecoAnimal enderecoAnimal);
    Task<EnderecoAnimal> UpdateEnderecoAnimalAsync(int id_endereco, EnderecoAnimal enderecoAnimal);
    Task<EnderecoAnimal> GetEnderecoAnimalIdAsync(int id_endereco);
    Task<EnderecoAnimal> DeleteEnderecoAnimalAsync(int id_endereco);
    Task<IEnumerable<EnderecoAnimal>> GetAllEnderecoAnimalAsync();
}

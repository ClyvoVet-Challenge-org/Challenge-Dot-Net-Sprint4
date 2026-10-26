using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Infrastruture.Interfaces.MongoRepository;

public interface IEnderecoAnimalMongoRepository
{
    Task<EnderecoAnimal> CreateEnderecoAnimalAsync(EnderecoAnimal enderecoAnimal);
    Task<EnderecoAnimal> UpdateEnderecoAnimalAsync(int id_endereco, EnderecoAnimal enderecoAnimal);
    Task<EnderecoAnimal> GetEnderecoAnimalIdAsync(int id_endereco);
    Task<EnderecoAnimal> DeleteEnderecoAnimalAsync(int id_endereco);
    Task<IEnumerable<EnderecoAnimal>> GetAllEnderecoAnimalAsync();
}

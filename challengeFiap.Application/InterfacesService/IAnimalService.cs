using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.InterfacesService;

public interface IAnimalService
{
    Task<Animal> CreateAnimalAsync(Animal animal);
    Task<Animal> UpdateAnimalAsync(int id_animal, Animal animal);
    Task<Animal> DeleteAnimalAsync(int id_animal);
    Task<Animal> GetAnimalAsync(int id_animal);
    Task<IEnumerable<Animal>> GetAllAnimalsAsync();
}

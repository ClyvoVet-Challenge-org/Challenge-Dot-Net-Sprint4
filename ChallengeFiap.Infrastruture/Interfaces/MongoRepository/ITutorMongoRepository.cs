using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Infrastruture.Interfaces.MongoRepository;

public interface ITutorMongoRepository
{
    Task<Tutor> CreateTutorAsync(Tutor tutor);
    Task<Tutor> UpdateTutorAsync(int id_tutor, Tutor tutor);
    Task<Tutor> GetTutorIdAsync(int id_tutor);
    Task<Tutor> DeleteTutorAsync(int id_tutor);
    Task<IEnumerable<Tutor>> GetAllTutorAsync();
}

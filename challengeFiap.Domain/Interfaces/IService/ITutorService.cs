using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Interfaces.IService;

public interface ITutorService
{
    Task<Tutor> CreateTutorAsync(Tutor tutor);
    Task<Tutor> UpdateTutorAsync(int id_tutor, Tutor tutor);
    Task<Tutor> GetTutorIdAsync(int id_tutor);
    Task<Tutor> DeleteTutorAsync(int id_tutor);
    Task<IEnumerable<Tutor>> GetAllTutorAsync();
}

using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.MongoRepository;

public interface IPrescricaoMongoRepository
{
    Task<Prescricao> CreatePrescricaoAsync(Prescricao prescricao);
    Task<Prescricao> UpdatePrescricaoAsync(int id_prescricao, Prescricao prescricao);
    Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao);
    Task<Prescricao> DeletePrescricaoAsync(int id_prescricao);
    Task<IEnumerable<Prescricao>> GetAllPrescricaoAsync();
}

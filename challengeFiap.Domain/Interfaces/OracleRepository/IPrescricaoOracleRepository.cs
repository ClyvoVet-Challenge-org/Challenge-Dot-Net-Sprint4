using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.OracleRepository;

public interface IPrescricaoOracleRepository
{
    Task<Prescricao> CreatePrescricaoAsync(Prescricao prescricao);
    Task<Prescricao> UpdatePrescricaoAsync(int id_prescricao, Prescricao prescricao);
    Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao);
    Task<Prescricao> DeletePrescricaoAsync(int id_prescricao);
    Task<IEnumerable<Prescricao>> GetAllPrescricaoAsync();
}

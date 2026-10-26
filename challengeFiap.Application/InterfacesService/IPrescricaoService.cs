using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.InterfacesService;

public interface IPrescricaoService
{
    Task<Prescricao> CreatePrescricaoAsync(Prescricao prescricao);
    Task<Prescricao> UpdatePrescricaoAsync(int id_prescricao, Prescricao prescricao);
    Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao);
    Task<Prescricao> DeletePrescricaoAsync(int id_prescricao);
    Task<IEnumerable<Prescricao>> GetAllPrescricaoAsync();
}

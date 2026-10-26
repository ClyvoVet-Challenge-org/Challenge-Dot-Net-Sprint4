using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.InterfacesService;

public interface IClinicaService
{
    Task<Clinica> CreateadAsync(Clinica clinica);
    Task<Clinica> UpdateClinicaAsync(int id_clinica, Clinica clinica);
    Task<Clinica> GetClinicaIdAsync(int id_Clinica);
    Task<Clinica> DeleteClinicaAsync(int id_Clinica);
    Task<IEnumerable<Clinica>> GetAllClinicaAsync();
}

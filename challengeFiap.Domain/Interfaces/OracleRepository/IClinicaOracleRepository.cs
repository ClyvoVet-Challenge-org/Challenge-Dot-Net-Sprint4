using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.OracleRepository;

public interface IClinicaOracleRepository
{
    Task<Clinica> CreateadAsync(Clinica clinica);
    Task<Clinica> UpdateClinicaAsync(int id_clinica, Clinica clinica);
    Task<Clinica> GetClinicaIdAsync(int id_Clinica);
    Task<Clinica> DeleteClinicaAsync(int id_Clinica);
    Task<IEnumerable<Clinica>> GetAllClinicaAsync();
}

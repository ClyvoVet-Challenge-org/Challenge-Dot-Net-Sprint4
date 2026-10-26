using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Infrastruture.Interfaces.OracleRepository;

public interface IVetClinicaOracleRepository
{
    Task<VetClinica> CreateVetClinicaAsync(VetClinica VetClinica);
    Task<VetClinica> UpdateVetClinicaAsync(int id_VetClinica, VetClinica VetClinica);
    Task<VetClinica> GetVetClinicaIdAsync(int id_VetClinica);
    Task<VetClinica> DeleteVetClinicaAsync(int id_VetClinica);
    Task<IEnumerable<VetClinica>> GetAllVetClinicaAsync();
}

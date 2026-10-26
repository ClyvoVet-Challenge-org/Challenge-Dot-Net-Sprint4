using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.Interfaces;

public interface IVetClinicaService
{
    Task<VetClinica> CreateVetClinicaAsync(VetClinica VetClinica);
    Task<VetClinica> UpdateVetClinicaAsync(int id_VetClinica, VetClinica VetClinica);
    Task<VetClinica> GetVetClinicaIdAsync(int id_VetClinica);
    Task<VetClinica> DeleteVetClinicaAsync(int id_VetClinica);
    Task<IEnumerable<VetClinica>> GetAllVetClinicaAsync();
}

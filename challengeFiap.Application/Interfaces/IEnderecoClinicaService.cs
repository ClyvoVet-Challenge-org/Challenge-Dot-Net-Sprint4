using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.Interfaces;

public interface IEnderecoClinicaService
{
    Task<EnderecoClinica> CreateEnderecoClinicaAsync(EnderecoClinica enderecoClinica);
    Task<EnderecoClinica> UpdateEnderecoClinicaAsync(int id_endereco_clinica, EnderecoClinica enderecoClinica);
    Task<EnderecoClinica> GetEnderecoClinicaIdAsync(int id_endereco_clinica);
    Task<EnderecoClinica> DeleteEnderecoClinicaAsync(int id_endereco_clinica);
    Task<IEnumerable<EnderecoClinica>> GetAllEnderecoClinicaAsync();
}

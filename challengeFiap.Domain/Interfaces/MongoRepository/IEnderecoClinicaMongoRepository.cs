using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.MongoRepository;

public interface IEnderecoClinicaMongoRepository
{
    Task<EnderecoClinica> CreateEnderecoClinicaAsync(EnderecoClinica enderecoClinica);
    Task<EnderecoClinica> UpdateEnderecoClinicaAsync(int id_endereco_clinica, EnderecoClinica enderecoClinica);
    Task<EnderecoClinica> GetEnderecoClinicaIdAsync(int id_endereco_clinica);
    Task<EnderecoClinica> DeleteEnderecoClinicaAsync(int id_endereco_clinica);
    Task<IEnumerable<EnderecoClinica>> GetAllEnderecoClinicaAsync();
}

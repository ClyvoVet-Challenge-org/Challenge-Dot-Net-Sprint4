using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.OracleRepository;

public interface IEnderecoTutorOracleRepository
{
    Task<EnderecoTutor> CreateEnderecoTutorAsync(EnderecoTutor enderecoTutor);
    Task<EnderecoTutor> UpdateEnderecoTutorAsync(int id_endereco, EnderecoTutor enderecoTutor);
    Task<EnderecoTutor> GetEnderecoTutorIdAsync(int id_endereco);
    Task<EnderecoTutor> DeleteEnderecoTutorAsync(int id_endereco);
    Task<IEnumerable<EnderecoTutor>> GetAllEnderecoTutorAsync();
}

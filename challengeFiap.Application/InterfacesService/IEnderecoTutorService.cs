using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.InterfacesService;

public interface IEnderecoTutorService
{
    Task<EnderecoTutor> CreateEnderecoTutorAsync(EnderecoTutor enderecoTutor);
    Task<EnderecoTutor> UpdateEnderecoTutorAsync(int id_endereco, EnderecoTutor enderecoTutor);
    Task<EnderecoTutor> GetEnderecoTutorIdAsync(int id_endereco);
    Task<EnderecoTutor> DeleteEnderecoTutorAsync(int id_endereco);
    Task<IEnumerable<EnderecoTutor>> GetAllEnderecoTutorAsync();
}

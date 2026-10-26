
using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class EnderecoTutorService : IEnderecoTutorService
    {
        private readonly IEnderecoTutorOracleRepository _oracle;
        private readonly IEnderecoTutorMongoRepository _mongo;

        public EnderecoTutorService(IEnderecoTutorOracleRepository oracle, IEnderecoTutorMongoRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<EnderecoTutor> CreateEnderecoTutorAsync(EnderecoTutor enderecoTutor)
        {
            var created = await _oracle.CreateEnderecoTutorAsync(enderecoTutor);
            await _mongo.CreateEnderecoTutorAsync(created);
            return created;
        }

        public async Task<IEnumerable<EnderecoTutor>> GetAllEnderecoTutorAsync()
        {
            var oracleItems = await _oracle.GetAllEnderecoTutorAsync();
            var mongoItems = await _mongo.GetAllEnderecoTutorAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_endereco_tutor);
        }

        public Task<EnderecoTutor> GetEnderecoTutorIdAsync(int id_endereco)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetEnderecoTutorIdAsync(id_endereco),
                () => _mongo.GetEnderecoTutorIdAsync(id_endereco));
        }

        public async Task<EnderecoTutor> UpdateEnderecoTutorAsync(int id_endereco, EnderecoTutor enderecoTutor)
        {
            var updated = await _oracle.UpdateEnderecoTutorAsync(id_endereco, enderecoTutor);
            await _mongo.UpdateEnderecoTutorAsync(id_endereco, updated);
            return updated;
        }

        public async Task<EnderecoTutor> DeleteEnderecoTutorAsync(int id_endereco)
        {
            var deleted = await _oracle.DeleteEnderecoTutorAsync(id_endereco);
            await _mongo.DeleteEnderecoTutorAsync(id_endereco);
            return deleted;
        }
    }
}

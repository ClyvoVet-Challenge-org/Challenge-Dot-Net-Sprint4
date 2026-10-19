
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class EnderecoTutorService : IenderecoTutorRepository
    {
        private readonly EnderecoTutorOracleRepository _oracle;
        private readonly EnderecoTutorRepository _mongo;

        public EnderecoTutorService(EnderecoTutorOracleRepository oracle, EnderecoTutorRepository mongo)
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
            var list = await _oracle.GetAllEnderecoTutorAsync();
            await _mongo.GetAllEnderecoTutorAsync();
            return list;
        }

        public async Task<EnderecoTutor> GetEnderecoTutorIdAsync(int id_endereco)
        {
            var item = await _oracle.GetEnderecoTutorIdAsync(id_endereco);
            await _mongo.GetEnderecoTutorIdAsync(id_endereco);
            return item;
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


using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class EnderecoAnimalService : IenderecoAnimalRepository
    {
        private readonly EnderecoAnimalOracleRepository _oracle;
        private readonly EnderecoAnimalRepository _mongo;

        public EnderecoAnimalService(EnderecoAnimalOracleRepository oracle, EnderecoAnimalRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<EnderecoAnimal> CreateEnderecoAnimalAsync(EnderecoAnimal enderecoAnimal)
        {
            var created = await _oracle.CreateEnderecoAnimalAsync(enderecoAnimal);
            await _mongo.CreateEnderecoAnimalAsync(created);
            return created;
        }

        public async Task<IEnumerable<EnderecoAnimal>> GetAllEnderecoAnimalAsync()
        {
            var list = await _oracle.GetAllEnderecoAnimalAsync();
            await _mongo.GetAllEnderecoAnimalAsync();
            return list;
        }

        public async Task<EnderecoAnimal> GetEnderecoAnimalIdAsync(int id_endereco)
        {
            var item = await _oracle.GetEnderecoAnimalIdAsync(id_endereco);
            await _mongo.GetEnderecoAnimalIdAsync(id_endereco);
            return item;
        }

        public async Task<EnderecoAnimal> UpdateEnderecoAnimalAsync(int id_endereco, EnderecoAnimal enderecoAnimal)
        {
            var updated = await _oracle.UpdateEnderecoAnimalAsync(id_endereco, enderecoAnimal);
            await _mongo.UpdateEnderecoAnimalAsync(id_endereco, updated);
            return updated;
        }

        public async Task<EnderecoAnimal> DeleteEnderecoAnimalAsync(int id_endereco)
        {
            var deleted = await _oracle.DeleteEnderecoAnimalAsync(id_endereco);
            await _mongo.DeleteEnderecoAnimalAsync(id_endereco);
            return deleted;
        }
    }
}

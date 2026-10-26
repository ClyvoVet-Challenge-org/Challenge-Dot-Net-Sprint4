using challengeFiap.Domain.Entities;
using challengeFiap.Application.InterfacesService;
using challengeFiap.Infrastruture.Interfaces.MongoRepository;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;


namespace challengeFiap.Application.Service
{
    public class EnderecoAnimalService : IEnderecoAnimalService
    {
        private readonly IEnderecoAnimalOracleRepository _oracle;
        private readonly IEnderecoAnimalMongoRepository _mongo;

        public EnderecoAnimalService(IEnderecoAnimalOracleRepository oracle, IEnderecoAnimalMongoRepository mongo)
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
            var oracleItems = await _oracle.GetAllEnderecoAnimalAsync();
            var mongoItems = await _mongo.GetAllEnderecoAnimalAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_endereco_animal);
        }

        public Task<EnderecoAnimal> GetEnderecoAnimalIdAsync(int id_endereco)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetEnderecoAnimalIdAsync(id_endereco),
                () => _mongo.GetEnderecoAnimalIdAsync(id_endereco));
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

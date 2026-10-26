using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class EnderecoClinicaService : IEnderecoClinicaService
    {
        private readonly IEnderecoClinicaOracleRepository _oracle;
        private readonly IEnderecoClinicaMongoRepository _mongo;

        public EnderecoClinicaService(IEnderecoClinicaOracleRepository oracle, IEnderecoClinicaMongoRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<EnderecoClinica> CreateEnderecoClinicaAsync(EnderecoClinica enderecoClinica)
        {
            var created = await _oracle.CreateEnderecoClinicaAsync(enderecoClinica);
            await _mongo.CreateEnderecoClinicaAsync(created);
            return created;
        }

        public async Task<IEnumerable<EnderecoClinica>> GetAllEnderecoClinicaAsync()
        {
            var oracleItems = await _oracle.GetAllEnderecoClinicaAsync();
            var mongoItems = await _mongo.GetAllEnderecoClinicaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_endereco_clinica);
        }

        public async Task<EnderecoClinica> GetEnderecoClinicaIdAsync(int id_endereco_clinica)
        {
            try
            {
                var oracleItem = await _oracle.GetEnderecoClinicaIdAsync(id_endereco_clinica);
                try
                {
                    await _mongo.GetEnderecoClinicaIdAsync(id_endereco_clinica);
                }
                catch (KeyNotFoundException)
                {
                    // Prefer the Oracle record when MongoDB does not contain its copy.
                }
                return oracleItem;
            }
            catch (KeyNotFoundException)
            {
                return await _mongo.GetEnderecoClinicaIdAsync(id_endereco_clinica);
            }
        }

        public async Task<EnderecoClinica> UpdateEnderecoClinicaAsync(int id_endereco_clinica, EnderecoClinica enderecoClinica)
        {
            var updated = await _oracle.UpdateEnderecoClinicaAsync(id_endereco_clinica, enderecoClinica);
            await _mongo.UpdateEnderecoClinicaAsync(id_endereco_clinica, updated);
            return updated;
        }

        public async Task<EnderecoClinica> DeleteEnderecoClinicaAsync(int id_EnderecoClinica)
        {
            var deleted = await _oracle.DeleteEnderecoClinicaAsync(id_EnderecoClinica);
            await _mongo.DeleteEnderecoClinicaAsync(id_EnderecoClinica);
            return deleted;
        }
    }
}

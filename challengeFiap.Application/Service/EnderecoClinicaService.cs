using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace challengeFiap.Application.Service
{
    public class EnderecoClinicaService : IEnderecoClinicaService
    {
        private readonly IEnderecoClinicaOracleRepository _oracle;
        private readonly IEnderecoClinicaMongoRepository _mongo;
        private readonly ILogger<EnderecoClinicaService> _logger;

        public EnderecoClinicaService(
            IEnderecoClinicaOracleRepository oracle,
            IEnderecoClinicaMongoRepository mongo,
            ILogger<EnderecoClinicaService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<EnderecoClinica> CreateEnderecoClinicaAsync(EnderecoClinica enderecoClinica)
        {
            _logger.LogInformation("Criando endereço de clínica.");
            var created = await _oracle.CreateEnderecoClinicaAsync(enderecoClinica);
            await _mongo.CreateEnderecoClinicaAsync(created);
            return created;
        }

        public async Task<IEnumerable<EnderecoClinica>> GetAllEnderecoClinicaAsync()
        {
            _logger.LogInformation("Buscando todos os endereços de clínicas.");
            var oracleItems = await _oracle.GetAllEnderecoClinicaAsync();
            var mongoItems = await _mongo.GetAllEnderecoClinicaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_endereco_clinica);
        }

        public async Task<EnderecoClinica> GetEnderecoClinicaIdAsync(int id_endereco_clinica)
        {
            _logger.LogInformation("Buscando endereço de clínica por ID.");
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
            _logger.LogInformation("Atualizando endereço de clínica.");
            var updated = await _oracle.UpdateEnderecoClinicaAsync(id_endereco_clinica, enderecoClinica);
            await _mongo.UpdateEnderecoClinicaAsync(id_endereco_clinica, updated);
            return updated;
        }

        public async Task<EnderecoClinica> DeleteEnderecoClinicaAsync(int id_EnderecoClinica)
        {
            _logger.LogInformation("Excluindo endereço de clínica.");
            var deleted = await _oracle.DeleteEnderecoClinicaAsync(id_EnderecoClinica);
            await _mongo.DeleteEnderecoClinicaAsync(id_EnderecoClinica);
            return deleted;
        }
    }
}

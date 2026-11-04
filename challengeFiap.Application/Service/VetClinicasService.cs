using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using Microsoft.Extensions.Logging;

namespace challengeFiap.Application.Service
{
    public class VetClinicasService : IVetClinicaService
    {
        private readonly IVetClinicaOracleRepository _oracle;
        private readonly IVetClinicaMongoRepository _mongo;
        private readonly ILogger<VetClinicasService> _logger;

        public VetClinicasService(
            IVetClinicaOracleRepository oracle,
            IVetClinicaMongoRepository mongo,
            ILogger<VetClinicasService> logger)
        {
            _oracle = oracle;
            _mongo = mongo;
            _logger = logger;
        }

        public async Task<VetClinica> CreateVetClinicaAsync(VetClinica VetClinica)
        {
            _logger.LogInformation("Criando vínculo entre veterinário e clínica.");
            var created = await  _oracle.CreateVetClinicaAsync(VetClinica);
            await _mongo.CreateVetClinicaAsync(created);
            return created;
        }

        public async Task<IEnumerable<VetClinica>> GetAllVetClinicaAsync()
        {
            _logger.LogInformation("Buscando todos os vínculos entre veterinários e clínicas.");
            var oracleItems = await _oracle.GetAllVetClinicaAsync();
            var mongoItems = await _mongo.GetAllVetClinicaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_clinica_vet);
        }

        public Task<VetClinica> GetVetClinicaIdAsync(int id_VetClinica)
        {
            _logger.LogInformation("Buscando vínculo entre veterinário e clínica por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetVetClinicaIdAsync(id_VetClinica),
                () => _mongo.GetVetClinicaIdAsync(id_VetClinica));
        }

        public async Task<VetClinica> UpdateVetClinicaAsync(int id_VetClinica, VetClinica VetClinica)
        {
            _logger.LogInformation("Atualizando vínculo entre veterinário e clínica.");
            var updated = await _oracle.UpdateVetClinicaAsync(id_VetClinica, VetClinica);
            await _mongo.UpdateVetClinicaAsync(id_VetClinica, updated);
            return updated;
        }

        public async Task<VetClinica> DeleteVetClinicaAsync(int id_VetClinica)
        {
            _logger.LogInformation("Excluindo vínculo entre veterinário e clínica.");
            var deleted = await _oracle.DeleteVetClinicaAsync(id_VetClinica);
            await _mongo.DeleteVetClinicaAsync(id_VetClinica);
            return deleted;
        }
    }
}

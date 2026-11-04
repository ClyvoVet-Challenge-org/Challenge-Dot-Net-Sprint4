
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces.IService;
using challengeFiap.Domain.Interfaces.IMongoRepository;
using challengeFiap.Domain.Interfaces.IOracleRepository;
using challengeFiap.Infrastruture.Repositorie;
using Microsoft.Extensions.Logging;


namespace challengeFiap.Application.Service
{
    public class ClinicaService : IClinicaService
    {
        private readonly IClinicaOracleRepository clinicaOracle;
        private readonly IClinicaMongoRepository clinicaMongo;
        private readonly ILogger<ClinicaService> _logger;

        public ClinicaService(
            IClinicaOracleRepository clinicaOracle,
            IClinicaMongoRepository clinicaMongo,
            ILogger<ClinicaService> logger)
        {
            this.clinicaOracle = clinicaOracle;
            this.clinicaMongo = clinicaMongo;
            _logger = logger;
        }

        public async Task<Clinica> CreateadAsync(Clinica clinica)
        {
            _logger.LogInformation("Criando clínica.");
            var createdClinica =
                await clinicaOracle.CreateadAsync(clinica);

            await clinicaMongo.CreateadAsync(createdClinica);

            return createdClinica;
        }

        public async Task<IEnumerable<Clinica>> GetAllClinicaAsync()
        {
            _logger.LogInformation("Buscando todas as clínicas.");
            var oracleItems = await clinicaOracle.GetAllClinicaAsync();
            var mongoItems = await clinicaMongo.GetAllClinicaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_clinica);
        }

        public Task<Clinica> GetClinicaIdAsync(int id_Clinica)
        {
            _logger.LogInformation("Buscando clínica por ID.");
            return RepositoryResultMerger.FindByIdAsync(
                () => clinicaOracle.GetClinicaIdAsync(id_Clinica),
                () => clinicaMongo.GetClinicaIdAsync(id_Clinica));
        }

        public async Task<Clinica> UpdateClinicaAsync(
            int id_clinica, Clinica clinica)
        {
            _logger.LogInformation("Atualizando clínica.");
            var updatedClinica =
                await clinicaOracle.UpdateClinicaAsync(
                    id_clinica, clinica);

            await clinicaMongo.UpdateClinicaAsync(
                id_clinica, updatedClinica);

            return updatedClinica;
        }

        public async Task<Clinica> DeleteClinicaAsync(int id_Clinica)
        {
            _logger.LogInformation("Excluindo clínica.");
            var deletado = await clinicaOracle.DeleteClinicaAsync(id_Clinica);
            await clinicaMongo.DeleteClinicaAsync(id_Clinica);
            return deletado;
        }
    }
}

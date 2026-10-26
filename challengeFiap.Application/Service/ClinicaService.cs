
using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class ClinicaService : IClinicaService
    {
        private readonly IClinicaOracleRepository clinicaOracle;
        private readonly IClinicaMongoRepository clinicaMongo;

        public ClinicaService(
            IClinicaOracleRepository clinicaOracle,
            IClinicaMongoRepository clinicaMongo)
        {
            this.clinicaOracle = clinicaOracle;
            this.clinicaMongo = clinicaMongo;
        }

        public async Task<Clinica> CreateadAsync(Clinica clinica)
        {
            var createdClinica =
                await clinicaOracle.CreateadAsync(clinica);

            await clinicaMongo.CreateadAsync(createdClinica);

            return createdClinica;
        }

        public async Task<IEnumerable<Clinica>> GetAllClinicaAsync()
        {
            var oracleItems = await clinicaOracle.GetAllClinicaAsync();
            var mongoItems = await clinicaMongo.GetAllClinicaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_clinica);
        }

        public Task<Clinica> GetClinicaIdAsync(int id_Clinica)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => clinicaOracle.GetClinicaIdAsync(id_Clinica),
                () => clinicaMongo.GetClinicaIdAsync(id_Clinica));
        }

        public async Task<Clinica> UpdateClinicaAsync(
            int id_clinica, Clinica clinica)
        {
            var updatedClinica =
                await clinicaOracle.UpdateClinicaAsync(
                    id_clinica, clinica);

            await clinicaMongo.UpdateClinicaAsync(
                id_clinica, updatedClinica);

            return updatedClinica;
        }

        public async Task<Clinica> DeleteClinicaAsync(int id_Clinica)
        {
            var deletado = await clinicaOracle.DeleteClinicaAsync(id_Clinica);
            await clinicaMongo.DeleteClinicaAsync(id_Clinica);
            return deletado;
        }
    }
}

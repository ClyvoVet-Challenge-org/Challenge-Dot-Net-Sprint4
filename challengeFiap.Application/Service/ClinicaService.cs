
using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class ClinicaService : IClinicaRepository
    {
        private readonly ClinicaOracleRepository clinicaOracle;
        private readonly ClinicaRepository clinicaMongo;

        public ClinicaService(
            ClinicaOracleRepository clinicaOracle,
            ClinicaRepository clinicaMongo)
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
            var clinicas =
                await clinicaOracle.GetAllClinicaAsync();

            await clinicaMongo.GetAllClinicaAsync();

            return clinicas;
        }

        public async Task<Clinica> GetClinicaIdAsync(int id_Clinica)
        {
            var clinica =
                await clinicaOracle.GetClinicaIdAsync(id_Clinica);

            await clinicaMongo.GetClinicaIdAsync(id_Clinica);

            return clinica;
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
            var deletado =
                await clinicaOracle.DeleteClinicaAsync(id_Clinica);

            await clinicaMongo.DeleteClinicaAsync(id_Clinica);

            return deletado;
        }
    }
}

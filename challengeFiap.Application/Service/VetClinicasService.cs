using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class VetClinicasService : IVetClinicaService
    {
        private readonly IVetClinicaOracleRepository _oracle;
        private readonly IVetClinicaMongoRepository _mongo;

        public VetClinicasService(IVetClinicaOracleRepository oracle, IVetClinicaMongoRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<VetClinica> CreateVetClinicaAsync(VetClinica VetClinica)
        {
            var created = await  _oracle.CreateVetClinicaAsync(VetClinica);
            await _mongo.CreateVetClinicaAsync(created);
            return created;
        }

        public async Task<IEnumerable<VetClinica>> GetAllVetClinicaAsync()
        {
            var oracleItems = await _oracle.GetAllVetClinicaAsync();
            var mongoItems = await _mongo.GetAllVetClinicaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_clinica_vet);
        }

        public Task<VetClinica> GetVetClinicaIdAsync(int id_VetClinica)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetVetClinicaIdAsync(id_VetClinica),
                () => _mongo.GetVetClinicaIdAsync(id_VetClinica));
        }

        public async Task<VetClinica> UpdateVetClinicaAsync(int id_VetClinica, VetClinica VetClinica)
        {
            var updated = await _oracle.UpdateVetClinicaAsync(id_VetClinica, VetClinica);
            await _mongo.UpdateVetClinicaAsync(id_VetClinica, updated);
            return updated;
        }

        public async Task<VetClinica> DeleteVetClinicaAsync(int id_VetClinica)
        {
            var deleted = await _oracle.DeleteVetClinicaAsync(id_VetClinica);
            await _mongo.DeleteVetClinicaAsync(id_VetClinica);
            return deleted;
        }
    }
}

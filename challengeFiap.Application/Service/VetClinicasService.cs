using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class VetClinicasService : IVetClinica
    {
        private readonly VetClinicaOracleRepository _oracle;
        private readonly VetClinicaRepository _mongo;

        public VetClinicasService(VetClinicaOracleRepository oracle, VetClinicaRepository mongo)
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
            var list = await _oracle.GetAllVetClinicaAsync();
            await _mongo.GetAllVetClinicaAsync();
            return list;
        }

        public async Task<VetClinica> GetVetClinicaIdAsync(int id_VetClinica)
        {
            var item = await _oracle.GetVetClinicaIdAsync(id_VetClinica);
            await _mongo.GetVetClinicaIdAsync(id_VetClinica);
            return item;
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



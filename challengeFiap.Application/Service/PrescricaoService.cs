using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class PrescricaoService : IprescricaoRepository
    {
        private readonly PrescricaoOracleRepository _oracle;
        private readonly PrescricaoRepository _mongo;

        public PrescricaoService(PrescricaoOracleRepository oracle, PrescricaoRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<Prescricao> CreatePrescricaoAsync(Prescricao prescricao)
        {
            var created = await _oracle.CreatePrescricaoAsync(prescricao);
            await _mongo.CreatePrescricaoAsync(created);
            return created;
        }

        public async Task<IEnumerable<Prescricao>> GetAllPrescricaoAsync()
        {
            var list = await _oracle.GetAllPrescricaoAsync();
            await _mongo.GetAllPrescricaoAsync();
            return list;
        }

        public async Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao)
        {
            var item = await _oracle.GetPrescricaoIdAsync(id_prescricao);
            await _mongo.GetPrescricaoIdAsync(id_prescricao);
            return item;
        }

        public async Task<Prescricao> UpdatePrescricaoAsync(int id_prescricao, Prescricao prescricao)
        {
            var updated = await _oracle.UpdatePrescricaoAsync(id_prescricao, prescricao);
            await _mongo.UpdatePrescricaoAsync(id_prescricao, updated);
            return updated;
        }

        public async Task<Prescricao> DeletePrescricaoAsync(int id_prescricao)
        {
            var deleted = await _oracle.DeletePrescricaoAsync(id_prescricao);
            await _mongo.DeletePrescricaoAsync(id_prescricao);
            return deleted;
        }
    }
}

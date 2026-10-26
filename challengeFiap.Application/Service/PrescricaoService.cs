using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class PrescricaoService : IPrescricaoService
    {
        private readonly IPrescricaoOracleRepository _oracle;
        private readonly IPrescricaoMongoRepository _mongo;

        public PrescricaoService(IPrescricaoOracleRepository oracle, IPrescricaoMongoRepository mongo)
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
            var oracleItems = await _oracle.GetAllPrescricaoAsync();
            var mongoItems = await _mongo.GetAllPrescricaoAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_prescricao);
        }

        public Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetPrescricaoIdAsync(id_prescricao),
                () => _mongo.GetPrescricaoIdAsync(id_prescricao));
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

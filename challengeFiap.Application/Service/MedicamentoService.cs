using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IMedicamentoOracleRepository _oracle;
        private readonly IMedicamentoMongoRepository _mongo;

        public MedicamentoService(IMedicamentoOracleRepository oracle, IMedicamentoMongoRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<Medicamento> CreateMedicamentoAsync(Medicamento medicamento)
        {
            var created = await _oracle.CreateMedicamentoAsync(medicamento);
            await _mongo.CreateMedicamentoAsync(created);
            return created;
        }

        public async Task<IEnumerable<Medicamento>> GetAllMedicamentoAsync()
        {
            var oracleItems = await _oracle.GetAllMedicamentoAsync();
            var mongoItems = await _mongo.GetAllMedicamentoAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_medicamento);
        }

        public Task<Medicamento> GetMedicamentoIdAsync(int id_medicamento)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetMedicamentoIdAsync(id_medicamento),
                () => _mongo.GetMedicamentoIdAsync(id_medicamento));
        }

        public async Task<Medicamento> UpdateMedicamentoAsync(int id_medicamento, Medicamento medicamento)
        {
            var updated = await _oracle.UpdateMedicamentoAsync(id_medicamento, medicamento);
            await _mongo.UpdateMedicamentoAsync(id_medicamento, updated);
            return updated;
        }

        public async Task<Medicamento> DeleteMedicamentoAsync(int id_medicamento)
        {
            var deleted = await _oracle.DeleteMedicamentoAsync(id_medicamento);
            await _mongo.DeleteMedicamentoAsync(id_medicamento);
            return deleted;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class MedicamentoService : IMedicamentoRepository
    {
        private readonly MedicamentoOracleRepository _oracle;
        private readonly MedicamentoRepository _mongo;

        public MedicamentoService(MedicamentoOracleRepository oracle, MedicamentoRepository mongo)
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
            var list = await _oracle.GetAllMedicamentoAsync();
            await _mongo.GetAllMedicamentoAsync();
            return list;
        }

        public async Task<Medicamento> GetMedicamentoIdAsync(int id_medicamento)
        {
            var item = await _oracle.GetMedicamentoIdAsync(id_medicamento);
            await _mongo.GetMedicamentoIdAsync(id_medicamento);
            return item;
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

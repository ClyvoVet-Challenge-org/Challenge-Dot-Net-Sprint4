using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Infrastruture.Interfaces.OracleRepository;

public interface IMedicamentoOracleRepository
{
    Task<Medicamento> CreateMedicamentoAsync(Medicamento medicamento);
    Task<Medicamento> UpdateMedicamentoAsync(int id_medicamento, Medicamento medicamento);
    Task<Medicamento> GetMedicamentoIdAsync(int id_medicamento);
    Task<Medicamento> DeleteMedicamentoAsync(int id_medicamento);
    Task<IEnumerable<Medicamento>> GetAllMedicamentoAsync();
}

using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.Interfaces;

public interface IMedicamentoService
{
    Task<Medicamento> CreateMedicamentoAsync(Medicamento medicamento);
    Task<Medicamento> UpdateMedicamentoAsync(int id_medicamento, Medicamento medicamento);
    Task<Medicamento> GetMedicamentoIdAsync(int id_medicamento);
    Task<Medicamento> DeleteMedicamentoAsync(int id_medicamento);
    Task<IEnumerable<Medicamento>> GetAllMedicamentoAsync();
}

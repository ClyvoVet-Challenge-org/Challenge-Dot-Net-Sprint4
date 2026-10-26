using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class MedicamentoOracleRepository : IMedicamentoOracleRepository
    {
        private readonly AppDbContext _context;

        public MedicamentoOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Medicamento> CreateMedicamentoAsync(Medicamento medicamento)
        {
            await _context.Medicamentos.AddAsync(medicamento);
            await _context.SaveChangesAsync();
            return medicamento;
        }

        public async Task<Medicamento> DeleteMedicamentoAsync(int id_medicamento)
        {
            var med = _context.Medicamentos.Find(id_medicamento);
            if (med == null)
            {
                throw new KeyNotFoundException($"Medicamento com id {id_medicamento} não encontrado.");
            }
            _context.Medicamentos.Remove(med);
            await _context.SaveChangesAsync();
            return med;
        }

        public async Task<IEnumerable<Medicamento>> GetAllMedicamentoAsync()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        public async Task<Medicamento> GetMedicamentoIdAsync(int id_medicamento)
        {
            var med = await _context.Medicamentos.FindAsync(id_medicamento);
            if (med == null)
            {
                throw new KeyNotFoundException($"Medicamento com id {id_medicamento} não encontrado.");
            }
            return med;
        }

        public async Task<Medicamento> UpdateMedicamentoAsync(int id_medicamento, Medicamento medicamento)
        {
            var existe = await _context.Medicamentos.FindAsync(id_medicamento);
            if (existe == null)
            {
                throw new KeyNotFoundException($"Medicamento com id {id_medicamento} não encontrado.");
            }

            existe.Id_prescricao = medicamento.Id_prescricao;
            existe.Nm_medicamento = medicamento.Nm_medicamento;
            existe.Dosagem_medicamento = medicamento.Dosagem_medicamento;
            existe.Frequencia = medicamento.Frequencia;
            existe.Qtd_dias = medicamento.Qtd_dias;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

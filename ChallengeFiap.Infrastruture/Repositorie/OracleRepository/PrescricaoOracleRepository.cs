using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class PrescricaoOracleRepository : IprescricaoRepository
    {
        private readonly AppDbContext _context;

        public PrescricaoOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Prescricao> CreatePrescricaoAsync(Prescricao prescricao)
        {
            await _context.Prescricaos.AddAsync(prescricao);
            await _context.SaveChangesAsync();
            return prescricao;
        }

        public async Task<Prescricao> DeletePrescricaoAsync(int id_prescricao)
        {
            var pres = _context.Prescricaos.Find(id_prescricao);
            if (pres == null)
            {
                throw new KeyNotFoundException($"Prescricao com id {id_prescricao} não encontrado.");
            }
            _context.Prescricaos.Remove(pres);
            await _context.SaveChangesAsync();
            return pres;
        }

        public async Task<IEnumerable<Prescricao>> GetAllPrescricaoAsync()
        {
            return await _context.Prescricaos.ToListAsync();
        }

        public async Task<Prescricao> GetPrescricaoIdAsync(int id_prescricao)
        {
            var pres = await _context.Prescricaos.FindAsync(id_prescricao);
            if (pres == null)
            {
                throw new KeyNotFoundException($"Prescricao com id {id_prescricao} não encontrado.");
            }
            return pres;
        }

        public async Task<Prescricao> UpdatePrescricaoAsync(int id_prescricao, Prescricao prescricao)
        {
            var existe = await _context.Prescricaos.FindAsync(id_prescricao);
            if (existe == null)
            {
                throw new KeyNotFoundException($"Prescricao com id {id_prescricao} não encontrado.");
            }

            existe.Dt_emissao = prescricao.Dt_emissao;
            existe.Dt_expiracao = prescricao.Dt_expiracao;
            existe.Id_consulta = prescricao.Id_consulta;
            existe.Observacoes_gerais = prescricao.Observacoes_gerais;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

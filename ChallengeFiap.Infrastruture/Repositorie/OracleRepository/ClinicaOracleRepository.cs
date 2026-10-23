using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class ClinicaOracleRepository : IClinicaRepository
    {
        private readonly AppDbContext _context;

        public ClinicaOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Clinica> CreateadAsync(Clinica clinica)
        {
            await _context.Clinicas.AddAsync(clinica);
            await _context.SaveChangesAsync();
            return clinica;
        }

        public async Task<Clinica> DeleteClinicaAsync(int id_Clinica)
        {
            var clinica = _context.Clinicas.Find(id_Clinica);
            if (clinica == null)
            {
                throw new KeyNotFoundException($"Clinica com id {id_Clinica} não encontrado.");
            }
            _context.Clinicas.Remove(clinica);
            await _context.SaveChangesAsync();
            return clinica;
        }

        public async Task<IEnumerable<Clinica>> GetAllClinicaAsync()
        {
            return await _context.Clinicas.ToListAsync();
        }

        public async Task<Clinica> GetClinicaIdAsync(int id_Clinica)
        {
            var clinica = await _context.Clinicas.FindAsync(id_Clinica);
            if (clinica == null)
            {
                throw new KeyNotFoundException($"Clinica com id {id_Clinica} não encontrado.");
            }

            return clinica;
        }

        public async Task<Clinica> UpdateClinicaAsync(int id_clinica, Clinica clinica)
        {
            var existe = await _context.Clinicas.FindAsync(id_clinica);
            if (existe == null)
            {
                throw new KeyNotFoundException($"Clinica com id {id_clinica} não encontrado.");
            }

            existe.Cnpj_clinica = clinica.Cnpj_clinica;
            existe.Nm_clinica = clinica.Nm_clinica;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.OracleRepository;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class TutorOracleRepository : ITutorOracleRepository
    {
        private readonly AppDbContext _context;

        public TutorOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tutor> CreateTutorAsync(Tutor tutor)
        {
            await _context.Tutor.AddAsync(tutor);
            await _context.SaveChangesAsync();
            return tutor;
        }

        public async Task<Tutor> DeleteTutorAsync(int id_tutor)
        {
            var tutor = _context.Tutor.Find(id_tutor);
            if (tutor == null)
            {
                throw new KeyNotFoundException($"Tutor com id {id_tutor} não encontrado.");
            }
            _context.Tutor.Remove(tutor);
            await _context.SaveChangesAsync();
            return tutor;
        }

        public async Task<IEnumerable<Tutor>> GetAllTutorAsync()
        {
            return await _context.Tutor.ToListAsync();
        }

        public async Task<Tutor> GetTutorIdAsync(int id_tutor)
        {
            var tutor = await _context.Tutor.FindAsync(id_tutor);
            if (tutor == null)
            {
                throw new KeyNotFoundException($"Tutor com id {id_tutor} não encontrado.");
            }
            return tutor;
        }

        public async Task<Tutor> UpdateTutorAsync(int id_tutor, Tutor tutor)
        {
            var existe = await _context.Tutor.FindAsync(id_tutor);
            if (existe == null)
            {
                throw new KeyNotFoundException($"Tutor com id {id_tutor} não encontrado.");
            }

            existe.Cpf_tutor = tutor.Cpf_tutor;
            existe.Nm_tutor = tutor.Nm_tutor;
            existe.Nr_telefone_tutor = tutor.Nr_telefone_tutor;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

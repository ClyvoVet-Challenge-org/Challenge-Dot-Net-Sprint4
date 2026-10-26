using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class ConsultaOracleRepository : IConsultaOracleRepository
    {
        private readonly AppDbContext _context;

        public ConsultaOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Consulta> CreateConsultaAsync(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }

        public async Task<Consulta> DeleteConsultaAsync(int id_consulta)
        {
            var consulta = _context.Consultas.Find(id_consulta);
            if (consulta == null)
            {
                throw new KeyNotFoundException($"Consulta com id {id_consulta} não encontrado.");
            }
            _context.Consultas.Remove(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }

        public async Task<IEnumerable<Consulta>> GetAllConsultaAsync()
        {
            return await _context.Consultas.ToListAsync();
        }

        public async Task<Consulta> GetConsultaIdAsync(int id_consulta)
        {
            var consulta = await _context.Consultas.FindAsync(id_consulta);
            if (consulta == null)
            {
                throw new KeyNotFoundException($"Consulta com id {id_consulta} não encontrado.");
            }
            return consulta;
        }

        public async Task<Consulta> UpdateConsultaAsync(int id_consulta, Consulta consulta)
        {
            var existe = await _context.Consultas.FindAsync(id_consulta);
            if (existe == null)
            {
                throw new KeyNotFoundException($"Consulta com id {id_consulta} não encontrado.");
            }

            existe.Historico_consulta = consulta.Historico_consulta;
            existe.St_consulta = consulta.St_consulta;
            existe.Dt_consulta = consulta.Dt_consulta;
            existe.Id_vet = consulta.Id_vet;
            existe.Id_animal = consulta.Id_animal;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

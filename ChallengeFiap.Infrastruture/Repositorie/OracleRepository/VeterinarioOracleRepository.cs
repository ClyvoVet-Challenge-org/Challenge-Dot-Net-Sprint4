using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class VeterinarioOracleRepository : IVeterinarioRepository
    {
        private readonly AppDbContext _context;

        public VeterinarioOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Veterinario> CreateVeterinarioAsync(Veterinario veterinario)
        {
            await _context.Veterinarios.AddAsync(veterinario);
            await _context.SaveChangesAsync();
            return veterinario;
        }

        public async Task<Veterinario> DeleteVeterinarioAsync(int id_veterinario)
        {
            var vet = _context.Veterinarios.Find(id_veterinario);
            if (vet == null)
            {
                throw new KeyNotFoundException($"Veterinario com id {id_veterinario} não encontrado.");
            }
            _context.Veterinarios.Remove(vet);
            await _context.SaveChangesAsync();
            return vet;
        }

        public async Task<IEnumerable<Veterinario>> GetAllVeterinarioAsync()
        {
            return await _context.Veterinarios.ToListAsync();
        }

        public async Task<Veterinario> GetVeterinarioIdAsync(int id_veterinario)
        {
            var vet = await _context.Veterinarios.FindAsync(id_veterinario);
            if (vet == null)
            {
                throw new KeyNotFoundException($"Veterinario com id {id_veterinario} não encontrado.");
            }
            _context.Veterinarios.Remove(vet);
            await _context.SaveChangesAsync();
            return vet;
        }

        public async Task<Veterinario> UpdateVeterinarioAsync(int id_veterinario, Veterinario veterinario)
        {
            var existe = await _context.Veterinarios.FindAsync(id_veterinario);
            if (existe == null)
            {
                throw new KeyNotFoundException($"Veterinario com id {id_veterinario} não encontrado.");
            }

            existe.Nm_vet = veterinario.Nm_vet;
            existe.Cpf_vet = veterinario.Cpf_vet;
            existe.Crmv_vet = veterinario.Crmv_vet;
            existe.Email_vet = veterinario.Email_vet;
            existe.Senha_vet = veterinario.Senha_vet;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

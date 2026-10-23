using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class VetClinicaOracleRepository : IVetClinica
    {
        private readonly AppDbContext _context;

        public VetClinicaOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VetClinica> CreateVetClinicaAsync(VetClinica VetClinica)
        {
            await _context.VetClinicas.AddAsync(VetClinica);
            await _context.SaveChangesAsync();
            return VetClinica;
        }

        public async Task<VetClinica> DeleteVetClinicaAsync(int id_VetClinica)
        {
            var vetClinica = _context.VetClinicas.Find(id_VetClinica);
            if (vetClinica == null)
            {
                throw new KeyNotFoundException($"Vet Clinica com id {id_VetClinica} não foi encontrado");
            }
            _context.VetClinicas.Remove(vetClinica);
            await _context.SaveChangesAsync();
            return vetClinica;
        }

        public async Task<IEnumerable<VetClinica>> GetAllVetClinicaAsync()
        {
            return await _context.VetClinicas.ToListAsync();
        }

        public async Task<VetClinica> GetVetClinicaIdAsync(int id_VetClinica)
        {
            var existeVetClinica = await _context.VetClinicas.FindAsync(id_VetClinica);
            if (existeVetClinica == null)
            {
                throw new KeyNotFoundException($"VetClinica com id {id_VetClinica} não encontrado.");
            }

            return existeVetClinica;
        }

        public async Task<VetClinica> UpdateVetClinicaAsync(int id_VetClinica, VetClinica VetClinica)
        {
            var existe = await _context.VetClinicas.FindAsync(id_VetClinica);
            if (existe == null)
                throw new KeyNotFoundException($"VetClinica com id {id_VetClinica} não encontrado.");

            existe.Id_vet = VetClinica.Id_vet;
            existe.Id_clinica = VetClinica.Id_clinica;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class CarteiraVacinalOracleRepository : ICarteiraVacinalOracleRepository
    {
        private readonly AppDbContext _context;

        public CarteiraVacinalOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CarteiraVacinal> CreateCarteiraVacinalAsync(CarteiraVacinal carteiraVacinal)
        {
            await _context.CarteiraVacinals.AddAsync(carteiraVacinal);
            await _context.SaveChangesAsync();
            return carteiraVacinal;
        }

        public async Task<CarteiraVacinal> DeleteCarteiraVacinalAsync(int id_CarteiraVacinal)
        {
            var carteira = _context.CarteiraVacinals.Find(id_CarteiraVacinal);
            if (carteira == null)
            {
                throw new KeyNotFoundException($"CarteiraVacinal com id {id_CarteiraVacinal} não encontrado.");
            }
            _context.CarteiraVacinals.Remove(carteira);
            await _context.SaveChangesAsync();
            return carteira;
        }

        public async Task<IEnumerable<CarteiraVacinal>> GetAllCarteiraVacinalAsync()
        {
            return await _context.CarteiraVacinals.ToListAsync();
        }

        public async Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal)
        {
            var carteira = await _context.CarteiraVacinals.FindAsync(id_CarteiraVacinal);
            if (carteira == null)
            {
                throw new KeyNotFoundException($"CarteiraVacinal com id {id_CarteiraVacinal} não encontrado.");
            }
            return carteira;
        }

        public async Task<CarteiraVacinal> UpdateCarteiraVacinalAsync(int id_carteira, CarteiraVacinal carteiraVacinal)
        {
            var existe = await _context.CarteiraVacinals.FindAsync(id_carteira);
            if (existe == null)
            {
                throw new KeyNotFoundException($"CarteiraVacinal com id {id_carteira} não encontrado.");
            }

            existe.Nm_vacina = carteiraVacinal.Nm_vacina;
            existe.Dt_vacina_prevista = carteiraVacinal.Dt_vacina_prevista;
            existe.Dt_vacina_efetuada = carteiraVacinal.Dt_vacina_efetuada;
            existe.St_vacina = carteiraVacinal.St_vacina;
            existe.Id_animal = carteiraVacinal.Id_animal;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

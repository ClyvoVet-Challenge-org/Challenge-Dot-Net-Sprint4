using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class AnimalOracleRepository : IAnimalOracleRepository
    {
        private readonly AppDbContext _context;

        public AnimalOracleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Animal> CreateAnimalAsync(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> DeleteAnimalAsync(int id_animal)
        {
            var animal = _context.Animals.Find(id_animal);
            if (animal == null)
            {
                throw new KeyNotFoundException($"Animal com id {id_animal} não encontrado.");
            }
            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<IEnumerable<Animal>> GetAllAnimalsAsync()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<Animal> GetAnimalAsync(int id_animal)
        {
            var existeAnimal = await _context.Animals.FindAsync(id_animal);
            if (existeAnimal == null)
            {
                throw new KeyNotFoundException($"Animal com id {id_animal} não encontrado.");
            }
            return existeAnimal;
        }

        public async Task<Animal> UpdateAnimalAsync(int id_animal, Animal animal)
        {
            var existeAnimal = await _context.Animals.FindAsync(id_animal);
            if (existeAnimal == null)
            {
                throw new KeyNotFoundException($"Animal com id {id_animal} não encontrado.");
            }

            existeAnimal.Rg_animal = animal.Rg_animal;
            existeAnimal.Nr_microchip_animal = animal.Nr_microchip_animal;
            existeAnimal.Nm_animal = animal.Nm_animal;
            existeAnimal.Dt_nascimento_animal = animal.Dt_nascimento_animal;
            existeAnimal.Peso_animal = animal.Peso_animal;
            existeAnimal.Especie_animal = animal.Especie_animal;
            existeAnimal.Raca_animal = animal.Raca_animal;
            existeAnimal.Id_tutor = animal.Id_tutor;

            await _context.SaveChangesAsync();

            return existeAnimal;
        }
    }
}

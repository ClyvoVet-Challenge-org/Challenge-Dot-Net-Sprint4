using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class EnderecoAnimalOracleRepository : IenderecoAnimalRepository
    {
        private readonly AppDbContext _context;

        public EnderecoAnimalOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EnderecoAnimal> CreateEnderecoAnimalAsync(EnderecoAnimal enderecoAnimal)
        {
            await _context.EnderecoAnimals.AddAsync(enderecoAnimal);
            await _context.SaveChangesAsync();
            return enderecoAnimal;
        }

        public async Task<EnderecoAnimal> DeleteEnderecoAnimalAsync(int id_endereco)
        {
            var endereco = _context.EnderecoAnimals.Find(id_endereco);
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoAnimal com id {id_endereco} não encontrado.");
            }
            _context.EnderecoAnimals.Remove(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<IEnumerable<EnderecoAnimal>> GetAllEnderecoAnimalAsync()
        {
            return await _context.EnderecoAnimals.ToListAsync();
        }

        public async Task<EnderecoAnimal> GetEnderecoAnimalIdAsync(int id_endereco)
        {
            var endereco = await _context.EnderecoAnimals.FindAsync(id_endereco);
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoAnimal com id {id_endereco} não encontrado.");
            }
            _context.EnderecoAnimals.Remove(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<EnderecoAnimal> UpdateEnderecoAnimalAsync(int id_endereco, EnderecoAnimal enderecoAnimal)
        {
            var existe = await _context.EnderecoAnimals.FindAsync(id_endereco);
            if (existe == null)
            {
                throw new KeyNotFoundException($"EnderecoAnimal com id {id_endereco} não encontrado.");
            }

            existe.Pais = enderecoAnimal.Pais;
            existe.Estado = enderecoAnimal.Estado;
            existe.Cidade = enderecoAnimal.Cidade;
            existe.Bairro = enderecoAnimal.Bairro;
            existe.Logradouro_rua = enderecoAnimal.Logradouro_rua;
            existe.Nr_rua = enderecoAnimal.Nr_rua;
            existe.Complemento = enderecoAnimal.Complemento;
            existe.Cep = enderecoAnimal.Cep;
            existe.Id_animal = enderecoAnimal.Id_animal;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

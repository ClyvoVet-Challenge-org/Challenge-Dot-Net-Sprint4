using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class EnderecoTutorOracleRepository : IenderecoTutorRepository
    {
        private readonly AppDbContext _context;

        public EnderecoTutorOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EnderecoTutor> CreateEnderecoTutorAsync(EnderecoTutor enderecoTutor)
        {
            await _context.EnderecoTutors.AddAsync(enderecoTutor);
            await _context.SaveChangesAsync();
            return enderecoTutor;
        }

        public async Task<EnderecoTutor> DeleteEnderecoTutorAsync(int id_endereco)
        {
            var endereco = _context.EnderecoTutors.Find(id_endereco);
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoTutor com id {id_endereco} não encontrado.");
            }
            _context.EnderecoTutors.Remove(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<IEnumerable<EnderecoTutor>> GetAllEnderecoTutorAsync()
        {
            return await _context.EnderecoTutors.ToListAsync();
        }

        public async Task<EnderecoTutor> GetEnderecoTutorIdAsync(int id_endereco)
        {
            var endereco = await _context.EnderecoTutors.FindAsync(id_endereco);
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoTutor com id {id_endereco} não encontrado.");
            }
            return endereco;
        }

        public async Task<EnderecoTutor> UpdateEnderecoTutorAsync(int id_endereco, EnderecoTutor enderecoTutor)
        {
            var existe = await _context.EnderecoTutors.FindAsync(id_endereco);
            if (existe == null)
            {
                throw new KeyNotFoundException($"EnderecoTutor com id {id_endereco} não encontrado.");
            }

            existe.Pais = enderecoTutor.Pais;
            existe.Estado = enderecoTutor.Estado;
            existe.Cidade = enderecoTutor.Cidade;
            existe.Bairro = enderecoTutor.Bairro;
            existe.Logradouro_rua = enderecoTutor.Logradouro_rua;
            existe.Nr_rua = enderecoTutor.Nr_rua;
            existe.Complemento = enderecoTutor.Complemento;
            existe.Cep = enderecoTutor.Cep;
            existe.Id_tutor = enderecoTutor.Id_tutor;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

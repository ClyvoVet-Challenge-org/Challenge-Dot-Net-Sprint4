using challengeFiap.Domain.Entities;
using challengeFiap.Infrastruture.Interfaces.OracleRepository;
using challengeFiap.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeFiap.Infrastruture.Repositorie.OracleRepository
{
    public class EnderecoClinicaOracleRepository : IEnderecoClinicaOracleRepository
    {
        private readonly AppDbContext _context;

        public EnderecoClinicaOracleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EnderecoClinica> CreateEnderecoClinicaAsync(EnderecoClinica enderecoClinica)
        {
            await _context.EnderecoClinicas.AddAsync(enderecoClinica);
            await _context.SaveChangesAsync();
            return enderecoClinica;
        }

        public async Task<EnderecoClinica> DeleteEnderecoClinicaAsync(int id_endereco_clinica)
        {
            var endereco = _context.EnderecoClinicas.Find(id_endereco_clinica);
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoClinica com id {id_endereco_clinica} não encontrado.");
            }
            _context.EnderecoClinicas.Remove(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        public async Task<IEnumerable<EnderecoClinica>> GetAllEnderecoClinicaAsync()
        {
            return await _context.EnderecoClinicas.ToListAsync();
        }

        public async Task<EnderecoClinica> GetEnderecoClinicaIdAsync(int id_endereco_clinica)
        {
            var endereco = await _context.EnderecoClinicas.FindAsync(id_endereco_clinica);
            if (endereco == null)
            {
                throw new KeyNotFoundException($"EnderecoClinica com id {id_endereco_clinica} não encontrado.");
            }
            return endereco;
        }

        public async Task<EnderecoClinica> UpdateEnderecoClinicaAsync(int id_endereco_clinica, EnderecoClinica enderecoClinica)
        {
            var existe = await _context.EnderecoClinicas.FindAsync(id_endereco_clinica);
            if (existe == null)
            {
                throw new KeyNotFoundException($"EnderecoClinica com id {id_endereco_clinica} não encontrado.");
            }

            existe.Pais = enderecoClinica.Pais;
            existe.Estado = enderecoClinica.Estado;
            existe.Cidade = enderecoClinica.Cidade;
            existe.Bairro = enderecoClinica.Bairro;
            existe.Logradouro_rua = enderecoClinica.Logradouro_rua;
            existe.Nr_rua = enderecoClinica.Nr_rua;
            existe.Complemento = enderecoClinica.Complemento;
            existe.Cep = enderecoClinica.Cep;
            existe.Id_clinica = enderecoClinica.Id_clinica;

            await _context.SaveChangesAsync();
            return existe;
        }
    }
}

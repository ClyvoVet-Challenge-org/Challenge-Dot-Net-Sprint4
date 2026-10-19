using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class EnderecoClinicaService : IenderecoClinicaRepository
    {
        private readonly EnderecoClinicaOracleRepository _oracle;
        private readonly EnderecoClinicaRepository _mongo;

        public EnderecoClinicaService(EnderecoClinicaOracleRepository oracle, EnderecoClinicaRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<EnderecoClinica> CreateEnderecoClinicaAsync(EnderecoClinica enderecoClinica)
        {
            var created = await _oracle.CreateEnderecoClinicaAsync(enderecoClinica);
            await _mongo.CreateEnderecoClinicaAsync(created);
            return created;
        }

        public async Task<IEnumerable<EnderecoClinica>> GetAllEnderecoClinicaAsync()
        {
            var list = await _oracle.GetAllEnderecoClinicaAsync();
            await _mongo.GetAllEnderecoClinicaAsync();
            return list;
        }

        public async Task<EnderecoClinica> GetEnderecoClinicaIdAsync(int id_endereco_clinica)
        {
            var item = await _oracle.GetEnderecoClinicaIdAsync(id_endereco_clinica);
            await _mongo.GetEnderecoClinicaIdAsync(id_endereco_clinica);
            return item;
        }

        public async Task<EnderecoClinica> UpdateEnderecoClinicaAsync(int id_endereco_clinica, EnderecoClinica enderecoClinica)
        {
            var updated = await _oracle.UpdateEnderecoClinicaAsync(id_endereco_clinica, enderecoClinica);
            await _mongo.UpdateEnderecoClinicaAsync(id_endereco_clinica, updated);
            return updated;
        }

        public async Task<EnderecoClinica> DeleteEnderecoClinicaAsync(int id_EnderecoClinica)
        {
            var deleted = await _oracle.DeleteEnderecoClinicaAsync(id_EnderecoClinica);
            await _mongo.DeleteEnderecoClinicaAsync(id_EnderecoClinica);
            return deleted;
        }
    }
}


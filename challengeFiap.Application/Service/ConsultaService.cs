using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class ConsultaService : IConsultaRepository
    {
        private readonly ConsultaOracleRepository _oracle;
        private readonly ConsultaRepository _mongo;

        public ConsultaService(ConsultaOracleRepository oracle, ConsultaRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<Consulta> CreateConsultaAsync(Consulta consulta)
        {
            var created = await _oracle.CreateConsultaAsync(consulta);
            await _mongo.CreateConsultaAsync(created);
            return created;
        }

        public async Task<IEnumerable<Consulta>> GetAllConsultaAsync()
        {
            var list = await _oracle.GetAllConsultaAsync();

            await _mongo.GetAllConsultaAsync();

            return list;
        }

        public async Task<Consulta> GetConsultaIdAsync(int id_consulta)
        {
            var item = await _oracle.GetConsultaIdAsync(id_consulta);

            await _mongo.GetConsultaIdAsync(id_consulta);

            return item;
        }

        public async Task<Consulta> UpdateConsultaAsync(int id_consulta, Consulta consulta)
        {
            var updated = await _oracle.UpdateConsultaAsync(id_consulta, consulta);
            await _mongo.UpdateConsultaAsync(id_consulta, updated);
            return updated;
        }

        public async Task<Consulta> DeleteConsultaAsync(int id_consulta)
        {
            var deleted = await _oracle.DeleteConsultaAsync(id_consulta);
            await _mongo.DeleteConsultaAsync(id_consulta);
            return deleted;
        }
    }
}

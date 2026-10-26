using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaOracleRepository _oracle;
        private readonly IConsultaMongoRepository _mongo;

        public ConsultaService(IConsultaOracleRepository oracle, IConsultaMongoRepository mongo)
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
            var oracleItems = await _oracle.GetAllConsultaAsync();
            var mongoItems = await _mongo.GetAllConsultaAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_consulta);
        }

        public Task<Consulta> GetConsultaIdAsync(int id_consulta)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetConsultaIdAsync(id_consulta),
                () => _mongo.GetConsultaIdAsync(id_consulta));
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

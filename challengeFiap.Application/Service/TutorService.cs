using challengeFiap.Domain.Entities;
using challengeFiap.Application.Interfaces;
using challengeFiap.Domain.Interfaces;
using challengeFiap.Domain.Interfaces.MongoRepository;
using challengeFiap.Domain.Interfaces.OracleRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Application.Service
{
    public class TutorService : ITutorService
    {
        private readonly ITutorOracleRepository _oracle;
        private readonly ITutorMongoRepository _mongo;

        public TutorService(ITutorOracleRepository oracle, ITutorMongoRepository mongo)
        {
            _oracle = oracle;
            _mongo = mongo;
        }

        public async Task<Tutor> CreateTutorAsync(Tutor tutor)
        {
            var created = await _oracle.CreateTutorAsync(tutor);
            await _mongo.CreateTutorAsync(created);
            return created;
        }

        public async Task<IEnumerable<Tutor>> GetAllTutorAsync()
        {
            var oracleItems = await _oracle.GetAllTutorAsync();
            var mongoItems = await _mongo.GetAllTutorAsync();
            return RepositoryResultMerger.MergeById(
                oracleItems, mongoItems, item => item.Id_tutor);
        }

        public Task<Tutor> GetTutorIdAsync(int id_tutor)
        {
            return RepositoryResultMerger.FindByIdAsync(
                () => _oracle.GetTutorIdAsync(id_tutor),
                () => _mongo.GetTutorIdAsync(id_tutor));
        }

        public async Task<Tutor> UpdateTutorAsync(int id_tutor, Tutor tutor)
        {
            var updated = await _oracle.UpdateTutorAsync(id_tutor, tutor);
            await _mongo.UpdateTutorAsync(id_tutor, updated);
            return updated;
        }

        public async Task<Tutor> DeleteTutorAsync(int id_tutor)
        {
            var deleted = await _oracle.DeleteTutorAsync(id_tutor);
            await _mongo.DeleteTutorAsync(id_tutor);
            return deleted;
        }
    }
}

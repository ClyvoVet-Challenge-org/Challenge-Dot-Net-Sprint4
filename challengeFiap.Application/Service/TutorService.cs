using challengeFiap.Domain.Entities;
using challengeFiap.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChallengeFiap.Infrastruture.Repositorie.OracleRepository;
using ChallengeFiap.Infrastruture.Repositorie.MongoRepository;

namespace challengeFiap.Application.Service
{
    public class TutorService : ItutorRepository
    {
        private readonly TutorOracleRepository _oracle;
        private readonly TutorRepository _mongo;

        public TutorService(TutorOracleRepository oracle, TutorRepository mongo)
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
            var list = await _oracle.GetAllTutorAsync();
            await _mongo.GetAllTutorAsync();
            return list;
        }

        public async Task<Tutor> GetTutorIdAsync(int id_tutor)
        {
            var item = await _oracle.GetTutorIdAsync(id_tutor);
            await _mongo.GetTutorIdAsync(id_tutor);
            return item;
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

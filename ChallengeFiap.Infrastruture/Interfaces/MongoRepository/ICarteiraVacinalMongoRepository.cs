using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Infrastruture.Interfaces.MongoRepository;

public interface ICarteiraVacinalMongoRepository
{
    Task<CarteiraVacinal> CreateCarteiraVacinalAsync(CarteiraVacinal carteiraVacinal);
    Task<CarteiraVacinal> UpdateCarteiraVacinalAsync(int id_carteira, CarteiraVacinal carteiraVacinal);
    Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal);
    Task<CarteiraVacinal> DeleteCarteiraVacinalAsync(int id_CarteiraVacinal);
    Task<IEnumerable<CarteiraVacinal>> GetAllCarteiraVacinalAsync();
}

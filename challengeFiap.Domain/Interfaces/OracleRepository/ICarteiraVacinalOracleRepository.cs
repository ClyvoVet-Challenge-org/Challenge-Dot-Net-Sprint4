using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.OracleRepository;

public interface ICarteiraVacinalOracleRepository
{
    Task<CarteiraVacinal> CreateCarteiraVacinalAsync(CarteiraVacinal carteiraVacinal);
    Task<CarteiraVacinal> UpdateCarteiraVacinalAsync(int id_carteira, CarteiraVacinal carteiraVacinal);
    Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal);
    Task<CarteiraVacinal> DeleteCarteiraVacinalAsync(int id_CarteiraVacinal);
    Task<IEnumerable<CarteiraVacinal>> GetAllCarteiraVacinalAsync();
}

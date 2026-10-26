using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.Interfaces;

public interface ICarteiraVacinalService
{
    Task<CarteiraVacinal> CreateCarteiraVacinalAsync(CarteiraVacinal carteiraVacinal);
    Task<CarteiraVacinal> UpdateCarteiraVacinalAsync(int id_carteira, CarteiraVacinal carteiraVacinal);
    Task<CarteiraVacinal> GetCarteiraVacinalIdAsync(int id_CarteiraVacinal);
    Task<CarteiraVacinal> DeleteCarteiraVacinalAsync(int id_CarteiraVacinal);
    Task<IEnumerable<CarteiraVacinal>> GetAllCarteiraVacinalAsync();
}

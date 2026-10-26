using challengeFiap.Domain.Entities;

namespace challengeFiap.Application.InterfacesService;

public interface IConsultaService
{
    Task<Consulta> CreateConsultaAsync(Consulta consulta);
    Task<Consulta> UpdateConsultaAsync(int id_consulta, Consulta consulta);
    Task<Consulta> GetConsultaIdAsync(int id_consulta);
    Task<Consulta> DeleteConsultaAsync(int id_consulta);
    Task<IEnumerable<Consulta>> GetAllConsultaAsync();
}

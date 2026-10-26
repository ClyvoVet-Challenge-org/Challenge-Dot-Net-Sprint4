using challengeFiap.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challengeFiap.Domain.Interfaces.OracleRepository;

public interface IConsultaOracleRepository
{
    Task<Consulta> CreateConsultaAsync(Consulta consulta);
    Task<Consulta> UpdateConsultaAsync(int id_consulta, Consulta consulta);
    Task<Consulta> GetConsultaIdAsync(int id_consulta);
    Task<Consulta> DeleteConsultaAsync(int id_consulta);
    Task<IEnumerable<Consulta>> GetAllConsultaAsync();
}

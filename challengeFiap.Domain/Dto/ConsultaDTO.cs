using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class ConsultaDTO
    {
        public Consulta Consulta { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public ConsultaDTO(Consulta consulta, List<LinkDTO> dto)
        {
            Consulta = consulta;
            Dto = dto;
        }
    }
}

using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class ClinicaDTO
    {
        public Clinica Clinica { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public ClinicaDTO(Clinica clinica, List<LinkDTO> dto)
        {
            Clinica = clinica;
            Dto = dto;
        }
    }
}

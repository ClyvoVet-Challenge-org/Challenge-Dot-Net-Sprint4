using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class VetClinicaDTO
    {
        public VetClinica VetClinica { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public VetClinicaDTO(VetClinica vetClinica, List<LinkDTO> dto)
        {
            VetClinica = vetClinica;
            Dto = dto;
        }
    }
}

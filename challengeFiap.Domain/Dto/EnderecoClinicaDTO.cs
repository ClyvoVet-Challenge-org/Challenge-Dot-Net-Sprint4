using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class EnderecoClinicaDTO
    {
        public EnderecoClinica EnderecoClinica { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public EnderecoClinicaDTO(EnderecoClinica enderecoClinica, List<LinkDTO> dto)
        {
            EnderecoClinica = enderecoClinica;
            Dto = dto;
        }
    }
}

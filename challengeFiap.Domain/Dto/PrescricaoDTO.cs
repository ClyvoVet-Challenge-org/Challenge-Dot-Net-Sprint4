using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class PrescricaoDTO
    {
        public Prescricao Prescricao { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public PrescricaoDTO(Prescricao prescricao, List<LinkDTO> dto)
        {
            Prescricao = prescricao;
            Dto = dto;
        }
    }
}

using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class TutorDTO
    {
        public Tutor Tutor { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public TutorDTO(Tutor tutor, List<LinkDTO> dto)
        {
            Tutor = tutor;
            Dto = dto;
        }
    }
}

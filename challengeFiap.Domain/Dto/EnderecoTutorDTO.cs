using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class EnderecoTutorDTO
    {
        public EnderecoTutor EnderecoTutor { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public EnderecoTutorDTO(EnderecoTutor enderecoTutor, List<LinkDTO> dto)
        {
            EnderecoTutor = enderecoTutor;
            Dto = dto;
        }
    }
}

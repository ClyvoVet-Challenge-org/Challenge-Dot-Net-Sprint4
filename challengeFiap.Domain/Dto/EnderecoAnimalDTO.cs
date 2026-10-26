using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class EnderecoAnimalDTO
    {
        public EnderecoAnimal EnderecoAnimal { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public EnderecoAnimalDTO(EnderecoAnimal enderecoAnimal, List<LinkDTO> dto)
        {
            EnderecoAnimal = enderecoAnimal;
            Dto = dto;
        }
    }
}

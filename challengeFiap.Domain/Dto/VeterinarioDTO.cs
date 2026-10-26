using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class VeterinarioDTO
    {
        public Veterinario Veterinario { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public VeterinarioDTO(Veterinario veterinario, List<LinkDTO> dto)
        {
            Veterinario = veterinario;
            Dto = dto;
        }
    }
}

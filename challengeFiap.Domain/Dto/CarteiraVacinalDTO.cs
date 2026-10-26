using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class CarteiraVacinalDTO
    {
        public CarteiraVacinal CarteiraVacinal { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public CarteiraVacinalDTO(CarteiraVacinal carteiraVacinal, List<LinkDTO> dto)
        {
            CarteiraVacinal = carteiraVacinal;
            Dto = dto;
        }
    }
}

using challengeFiap.Domain.Entities;

namespace challengeFiap.Domain.Dto
{
    public class MedicamentoDTO
    {
        public Medicamento Medicamento { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public MedicamentoDTO(Medicamento medicamento, List<LinkDTO> dto)
        {
            Medicamento = medicamento;
            Dto = dto;
        }
    }
}

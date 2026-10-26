using challengeFiap.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace challengeFiap.Domain.Dto
{
    public class AnimalDTO
    {
        public Animal Animal { get; set; }

        public List<LinkDTO> Dto { get; set; }

        public AnimalDTO(Animal a, List<LinkDTO> dto)
        {
            Animal = a;
            Dto = dto;
        }
    }
}

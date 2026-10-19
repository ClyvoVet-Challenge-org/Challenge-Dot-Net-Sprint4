using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeFiap.Infrastruture.Data
{
    public class MongoStoreDataBaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public CollectionNames CollectionNames { get; set; } = null!;
    }

    public class CollectionNames
    {
        // Entidades presentes em challengeFiap.Domain.Entities
        public string Clinicas { get; set; } = null!;
        public string Animais { get; set; } = null!;
        public string Consultas { get; set; } = null!;
        public string CarteirasVacinais { get; set; } = null!;
        public string EnderecoAnimal { get; set; } = null!;
        public string EnderecoClinica { get; set; } = null!;
        public string EnderecoTutor { get; set; } = null!;
        public string Tutor { get; set; } = null!;
        public string VetClinicas { get; set; } = null!;
        public string Veterinarios { get; set; } = null!;
        public string Medicamentos { get; set; } = null!;
        public string Prescricoes { get; set; } = null!;
    }
}

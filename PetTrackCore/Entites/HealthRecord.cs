using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;
using PetTrackCore.Enums;

namespace PetTrackCore.Entites
{
    public class HealthRecord : BaseEntity
    {
        public DateTime RecordDate { get; set; }
        public string Description { get; set; }
        public HealthRecordType RecordType { get; set; }
        public string VaccineName { get; set; }  // Eğer aşı ise
        public int PetId { get; set; }
        public Pet Pet { get; set; } = new Pet();

    }
}

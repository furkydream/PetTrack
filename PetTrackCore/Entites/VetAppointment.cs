using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;

namespace PetTrackCore.Entites
{
    public class VetAppointment : BaseEntity
    {
       
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; } = new Pet();

    }
}

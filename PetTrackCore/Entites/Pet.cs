using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;

namespace PetTrackCore.Entites
{
    public class Pet : BaseEntity
    {
        
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int PetOwnerId { get; set; }




        public PetOwner PetOwner { get; set; }
        public TrackerDevice TrackerDevice { get; set; }

        public ICollection<HealthRecord> HealthRecords { get; set; } = new List<HealthRecord>();
        public ICollection<VetAppointment> VetAppointments { get; set; } = new List<VetAppointment>();
        public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    }
}

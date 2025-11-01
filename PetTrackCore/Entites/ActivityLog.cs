using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;
using PetTrackCore.Enums;

namespace PetTrackCore.Entites
{
    public class ActivityLog : BaseEntity
    {
        public DateTime ActivityDate { get; set; }
        public ActivityType ActivityType { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }  // KM cinsinden
        public int Duration { get; set; }     // Dakika cinsinden
        // Hangi Pet ile ilgili?
        public int PetId { get; set; }
        public Pet Pet { get; set; } = new Pet();
        public int TrackerDeviceId { get; set; }
        public TrackerDevice TrackerDevice  { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;
using PetTrackCore.ValueObjects;

namespace PetTrackCore.Entites
{
    public class TrackerDevice : BaseEntity
    {
        public string SerialNumber { get; set; }
        public double BatteryLevel { get; set; }

        // Cihazın son bilinen konumu bir "Value Object" olarak tutuluyor.
        public Location Location { get; set; } = new Location();

        // Bu cihaz hangi Pet'e ait? (İlişki)
        
        public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
        public int PetId { get; set; }
        public Pet Pet { get; set; } = new Pet();
    }
}

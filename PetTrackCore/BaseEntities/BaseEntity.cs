using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.Enums;

namespace PetTrackCore.BaseEntities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public void MarkAsActive()
        {
            if (Status == EntityStatus.Inactive) { Status = EntityStatus.Active; }
            CreatedDate = DateTime.Now;

        }
        public void MarkAsInactive()
        {
            if (Status == EntityStatus.Active) { Status = EntityStatus.Inactive; }
            DeletedDate = DateTime.Now;
        }
    }
}

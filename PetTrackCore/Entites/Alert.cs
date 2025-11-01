using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;
using PetTrackCore.Enums;

namespace PetTrackCore.Entites
{
    public class Alert : BaseEntity
    {
        public DateTime AlertDate { get; set; }
        public AlertType AlertType { get; set; }  
        public string Message { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}

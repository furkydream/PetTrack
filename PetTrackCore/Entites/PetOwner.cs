using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;

namespace PetTrackCore.Entites
{
    public class PetOwner : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.Entites;
using PetTrackDataAccess.Contexts;
using PetTrackDataAccess.Interfaces;

namespace PetTrackDataAccess.Concretes
{
    public class PetRepo : GenericRepo<Pet>, IPet
    {
        public PetRepo(PetDbContext context) : base(context)
        {
        }
    }
}

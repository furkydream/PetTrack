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
    public class HealthRecordRepo : GenericRepo<HealthRecord>, IHealthRecord
    {
        public HealthRecordRepo(PetDbContext context) : base(context)
        {
        }
    }
}

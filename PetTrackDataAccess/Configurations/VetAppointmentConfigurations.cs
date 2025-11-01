using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTrackCore.Entites;

namespace PetTrackDataAccess.Configurations
{
    public class VetAppointmentConfigurations : IEntityTypeConfiguration<VetAppointment>
    {
        public void Configure(EntityTypeBuilder<VetAppointment> builder)
        {
            throw new NotImplementedException();
        }
    }
}

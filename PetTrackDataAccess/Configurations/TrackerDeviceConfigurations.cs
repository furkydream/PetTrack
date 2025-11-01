using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTrackCore.Entites;
using PetTrackCore.ValueObjects;

namespace PetTrackDataAccess.Configurations
{
    public class TrackerDeviceConfigurations : IEntityTypeConfiguration<TrackerDevice>
    {
        public void Configure(EntityTypeBuilder<TrackerDevice> builder)
        {
            

            builder.HasMany(td => td.ActivityLogs)
                .WithOne(al => al.TrackerDevice)
                .HasForeignKey(al => al.TrackerDeviceId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.ComplexProperty(l => l.Location);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetTrackCore.Entites;

namespace PetTrackDataAccess.Configurations
{
    public class PetConfigurations : IEntityTypeConfiguration<Pet>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pet> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(p=>p.PetOwner)
                .WithMany(o=>o.Pets)
                .IsRequired()
                .HasForeignKey(f => f.PetOwnerId);

            builder.HasOne(p=>p.TrackerDevice)
                .WithOne(o=>o.Pet)
                .IsRequired()
                .HasForeignKey<TrackerDevice>(f=>f.PetId);

            builder.HasMany(m=>m.HealthRecords)
                .WithOne(o => o.Pet)
                .IsRequired()
                .HasForeignKey(f=>f.PetId);

            builder.HasMany(m => m.VetAppointments)
                .WithOne(o => o.Pet)
                .IsRequired()
                .HasForeignKey(f => f.PetId);

            builder.HasMany(m => m.Alerts)
                .WithOne(o => o.Pet)
                .IsRequired()
                .HasForeignKey(f => f.PetId);


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PetTrackCore.BaseEntities;
using PetTrackCore.Entites;
using PetTrackDataAccess.Configurations;


namespace PetTrackDataAccess.Contexts
{
    public class PetDbContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<TrackerDevice> TrackerDevices { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<VetAppointment> VetAppointments { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=PetTrackDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Bu satır, bu projedeki (assembly) IEntityTypeConfiguration arayüzünü
            // uygulayan TÜM konfigürasyon dosyalarını bulur ve otomatik olarak uygular.
            modelBuilder.ApplyConfiguration(new PetConfigurations());
            modelBuilder.ApplyConfiguration(new TrackerDeviceConfigurations());
        }
    }
}
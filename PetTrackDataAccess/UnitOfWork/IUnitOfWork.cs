using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackDataAccess.Interfaces;

namespace PetTrackDataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAlert Alert { get; }
        IActivityLog Log { get; }
        IHealthRecord HealthRecord { get; }
        IPet Pet { get; }
        IPetOwner PetOwner { get; }
        ITrackerDevice trackerDevice { get; }
        IVetAppointment VetAppointment { get; }

        Task<int> CommitAsync();
    }
}

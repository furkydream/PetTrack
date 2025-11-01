using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackCore.BaseEntities;
using PetTrackCore.Enums;

namespace PetTrackDataAccess.Interfaces
{
    public interface IGenericRepo<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(EntityStatus status = EntityStatus.Active);
        Task<T?> GetByIdAsync(int id, EntityStatus status = EntityStatus.Active);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id, bool hardDelete = false);
    }
}

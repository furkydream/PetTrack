using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetTrackCore.BaseEntities;
using PetTrackCore.Enums;
using PetTrackDataAccess.Contexts;
using PetTrackDataAccess.Interfaces;

namespace PetTrackDataAccess.Concretes
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly PetDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(PetDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id, bool hardDelete = false)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (hardDelete)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                entity.MarkAsInactive();
            }

            return true;
        }

        public async Task<List<T>> GetAllAsync(EntityStatus status = EntityStatus.Active)
        {
            return await _dbSet
                .Where(e => e.Status == status)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id, EntityStatus status = EntityStatus.Active)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.Id == id && e.Status == status);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
            return entity;
        }
    }
}

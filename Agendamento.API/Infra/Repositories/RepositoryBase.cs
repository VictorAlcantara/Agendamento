using Agendamento.API.Data;
using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected AppDbContext appDbContext;

        public RepositoryBase(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await appDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await appDbContext.Set<TEntity>().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await appDbContext.Set<TEntity>().AddAsync(entity);
            await appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            appDbContext.Entry(entity).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var entity = await GetAsync(id);

            return await RemoveAsync(entity);
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            appDbContext.Set<TEntity>().Remove(entity);
            var removed = await appDbContext.SaveChangesAsync();
            return removed > 0;
        }
    }
}

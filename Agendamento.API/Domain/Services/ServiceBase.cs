using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Interfaces.Services;
using Agendamento.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        private readonly IRepositoryBase<TEntity> repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await repository.GetAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await repository.AddAsync(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await repository.RemoveAsync(id);
        }
    }
}

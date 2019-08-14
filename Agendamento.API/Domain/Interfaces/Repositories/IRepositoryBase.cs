using Agendamento.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<bool> RemoveAsync(int id);

        Task<bool> RemoveAsync(TEntity entity);
    }
}

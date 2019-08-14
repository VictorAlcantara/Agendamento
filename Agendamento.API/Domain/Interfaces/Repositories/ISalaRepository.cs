using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Domain.Interfaces.Repositories
{
    public interface ISalaRepository : IRepositoryBase<Sala>
    {
        Task<IEnumerable<Sala>> GetByDescricaoAsync(string descricao);

        Task<Sala> GetSalaAgendasAsync(int salaId);
    }
}

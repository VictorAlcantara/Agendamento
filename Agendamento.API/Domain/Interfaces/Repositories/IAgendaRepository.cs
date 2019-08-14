using Agendamento.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Domain.Interfaces.Repositories
{
    public interface IAgendaRepository : IRepositoryBase<Agenda>
    {
        Task<IEnumerable<Agenda>> GetAgendaParaSalaAsync(int salaId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Domain.Interfaces.Services
{
    public interface ISalaService : IServiceBase<Sala>
    {
        Task<IEnumerable<Sala>> GetByDescricaoAsync(string descricao);
    }
}

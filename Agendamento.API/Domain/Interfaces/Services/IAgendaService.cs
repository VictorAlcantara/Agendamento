using Agendamento.API.Domain.Models;

namespace Agendamento.API.Domain.Interfaces.Services
{
    public interface IAgendaService : IServiceBase<Agenda>
    {
        bool ValidateSalaHorario(Agenda agenda);
    }
}

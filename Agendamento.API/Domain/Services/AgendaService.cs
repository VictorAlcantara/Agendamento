using Agendamento.API.Domain.Interfaces;
using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Interfaces.Services;
using Agendamento.API.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.API.Domain.Services
{
    public class AgendaService : ServiceBase<Agenda>, IAgendaService
    {
        private readonly IAgendaRepository agendaRepository;

        public AgendaService(IAgendaRepository agendaRepository) : base(agendaRepository)
        {
            this.agendaRepository = agendaRepository;
        }

        public bool ValidateSalaHorario(Agenda agenda)
        {
            var agendasParaSala = agendaRepository.GetAgendaParaSalaAsync(agenda.SalaId).Result.ToList();

            foreach (var outraAgenda in agendasParaSala)
            {
                if (outraAgenda.Id == agenda.Id)
                    continue;

                if (agenda.HorarioFim < outraAgenda.HorarioInicio)
                    continue;

                if (agenda.HorarioInicio > outraAgenda.HorarioFim)
                    continue;

                return false;
            }

            return true;
        }
    }
}

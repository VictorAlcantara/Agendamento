using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.Mock.Repositories
{
    public class MockAgendaRepository : IAgendaRepository
    {
        private readonly ISalaRepository salaRepository;
        private IList<Agenda> context = new List<Agenda>();

        public MockAgendaRepository(ISalaRepository salaRepository)
        {
            this.salaRepository = salaRepository;

            context.Add(new Agenda
            {
                Id = 1,
                HorarioInicio = new DateTime(2019, 1, 12),
                HorarioFim = new DateTime(2019, 1, 20),
                Sala = salaRepository.GetAsync(1).Result,
            });

            context.Add(new Agenda
            {
                Id = 2,
                HorarioInicio = new DateTime(2019, 1, 12),
                HorarioFim = new DateTime(2019, 1, 20),
                Sala = salaRepository.GetAsync(2).Result,
            });

            foreach (var item in context)
            {
                item.SalaId = item.Sala?.Id ?? 0;
            }
        }

        public Task<Agenda> AddAsync(Agenda entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Agenda>> GetAgendaParaSalaAsync(int salaId)
        {
            return Task.FromResult<IEnumerable<Agenda>>(context.Where(w => w.SalaId == salaId));
        }

        public Task<IEnumerable<Agenda>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Agenda> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Agenda entity)
        {
            throw new NotImplementedException();
        }

        public Task<Agenda> UpdateAsync(Agenda entity)
        {
            throw new NotImplementedException();
        }
    }
}

using Agendamento.API.Data;
using Agendamento.API.Domain.Interfaces;
using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.API.Infra.Repositories
{
    public class AgendaRepository : RepositoryBase<Agenda>, IAgendaRepository
    {
        private readonly ISalaRepository salaRepository;

        public AgendaRepository(AppDbContext appDbContext, ISalaRepository salaRepository) : base(appDbContext)
        {
            this.salaRepository = salaRepository;
        }

        public new async Task<IEnumerable<Agenda>> GetAllAsync()
        {
            return await appDbContext.Set<Agenda>().Include(i => i.Sala).ToListAsync();
        }

        public new async Task<Agenda> GetAsync(int id)
        {
            return await appDbContext.Set<Agenda>().Include(i => i.Sala).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Agenda>> GetAgendaParaSalaAsync(int salaId)
        {
            return await appDbContext.Set<Agenda>().Where(w => w.SalaId == salaId).ToListAsync();
        }

        public new async Task<Agenda> AddAsync(Agenda entity)
        {
            AddSalaToAgenda(entity);
            return await base.AddAsync(entity);
        }

        public new async Task<Agenda> UpdateAsync(Agenda entity)
        {
            AddSalaToAgenda(entity);
            return await base.UpdateAsync(entity);
        }

        private void AddSalaToAgenda(Agenda entity)
        {
            if (entity.Sala == null || entity.Sala.Id != entity.SalaId)
                entity.Sala = salaRepository.GetAsync(entity.SalaId).Result;
        }
    }
}

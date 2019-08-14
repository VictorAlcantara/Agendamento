using Agendamento.API.Data;
using Agendamento.API.Domain;
using Agendamento.API.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.API.Infra.Repositories
{
    public class SalaRepository : RepositoryBase<Sala>, ISalaRepository
    {
        public SalaRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<Sala>> GetByDescricaoAsync(string descricao)
        {
            return await appDbContext.Set<Sala>().Where(w => w.Descricao.ToLower() == descricao.ToLower()).ToListAsync();
        }

        public async Task<Sala> GetSalaAgendasAsync(int salaId)
        {
            return await appDbContext.Set<Sala>().Include(i => i.Agendas).FirstOrDefaultAsync(f => f.Id == salaId);
        }

        public new async Task<bool> RemoveAsync(int id)
        {
            var sala = await GetSalaAgendasAsync(id);
            return await RemoveAsync(sala);
        }
    }
}

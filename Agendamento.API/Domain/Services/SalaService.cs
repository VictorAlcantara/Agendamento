using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.API.Domain.Services
{
    public class SalaService : ServiceBase<Sala>, ISalaService
    {
        private readonly ISalaRepository salaRepository;

        public SalaService(ISalaRepository salaRepository) : base(salaRepository)
        {
            this.salaRepository = salaRepository;
        }

        public async Task<IEnumerable<Sala>> GetByDescricaoAsync(string descricao)
        {
            return await salaRepository.GetByDescricaoAsync(descricao);
        }
    }
}

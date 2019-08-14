using Agendamento.API.Domain;
using Agendamento.API.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.Mock.Repositories
{
    public class MockSalaRepository : ISalaRepository
    {
        private IList<Sala> context = new List<Sala>();

        public MockSalaRepository()
        {
            context.Add(new Sala
            {
                Id = 1,
                Descricao = "Sala 01"
            });

            context.Add(new Sala
            {
                Id = 2,
                Descricao = "Sala 02"
            });

            context.Add(new Sala
            {
                Id = 3,
                Descricao = "Sala 03"
            });
        }

        public Task<Sala> AddAsync(Sala entity)
        {
            context.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<IEnumerable<Sala>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Sala>>(context);
        }

        public Task<Sala> GetAsync(int id)
        {
            return Task.FromResult(context.FirstOrDefault(f => f.Id == id));
        }

        public Task<IEnumerable<Sala>> GetByDescricaoAsync(string descricao)
        {
            return Task.FromResult<IEnumerable<Sala>>(context.Where(f => f.Descricao.ToLower() == descricao.ToLower()));
        }

        public Task<Sala> GetSalaAgendasAsync(int salaId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveAsync(Sala entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Sala> UpdateAsync(Sala entity)
        {
            throw new System.NotImplementedException();
        }
    }
}

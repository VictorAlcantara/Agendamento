using Agendamento.API.Contracts.V1.Responses;
using Agendamento.API.Domain;
using Agendamento.API.Domain.Models;
using AutoMapper;

namespace Agendamento.API.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Sala, SalaResponse>();

            CreateMap<Agenda, AgendaResponse>()
                .ForMember(m => m.Sala, o => o.MapFrom(f => new SalaResponse { Id = f.Sala.Id, Descricao = f.Sala.Descricao }));
        }
    }
}

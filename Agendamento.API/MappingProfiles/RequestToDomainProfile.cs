using Agendamento.API.Contracts.V1.Requests;
using Agendamento.API.Domain;
using Agendamento.API.Domain.Models;
using AutoMapper;
using System;

namespace Agendamento.API.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateSalaRequest, Sala>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao.Trim()))
                .ForMember(dest => dest.Id, o => o.Ignore());

            CreateMap<UpdateSalaRequest, Sala>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao.Trim()));

            CreateMap<CreateAgendaRequest, Agenda>()
                .ForMember(dest => dest.HorarioInicio, opt => opt.MapFrom(src => new DateTime(src.HorarioInicio.Year, src.HorarioInicio.Month, src.HorarioInicio.Day, 0, 0, 0)))
                .ForMember(dest => dest.HorarioFim, opt => opt.MapFrom(src => new DateTime(src.HorarioFim.Year, src.HorarioFim.Month, src.HorarioFim.Day, 23, 59, 59)))
                .ForMember(dest => dest.Id, o => o.Ignore());

            CreateMap<UpdateAgendaRequest, Agenda>()
                .ForMember(dest => dest.HorarioInicio, opt => opt.MapFrom(src => new DateTime(src.HorarioInicio.Year, src.HorarioInicio.Month, src.HorarioInicio.Day, 0, 0, 0)))
                .ForMember(dest => dest.HorarioFim, opt => opt.MapFrom(src => new DateTime(src.HorarioFim.Year, src.HorarioFim.Month, src.HorarioFim.Day, 23, 59, 59)));
        }
    }
}

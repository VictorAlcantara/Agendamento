using Agendamento.API.Contracts.V1.Requests;
using Agendamento.API.Domain.Interfaces.Services;
using Agendamento.API.Domain.Models;
using AutoMapper;
using FluentValidation;

namespace Agendamento.API.Validators
{
    public class CreateAgendaRequestValidator : AbstractValidator<CreateAgendaRequest>
    {
        public CreateAgendaRequestValidator(IAgendaService agendaService, ISalaService salaService, IMapper mapper)
        {
            RuleFor(r => r.Titulo)
                .Must(p => !string.IsNullOrEmpty(p.Trim())).WithMessage("Campo [título] não deve ser vazio");

            RuleFor(r => r.HorarioInicio)
                .NotEmpty().WithMessage("Campo [Horário Início] é obrigatório");

            RuleFor(r => r.HorarioFim)
                .NotEmpty().WithMessage("Campo [Horário Fim] é obrigatório")
                .Must((entity, p) =>
                {
                    return agendaService.ValidateSalaHorario(mapper.Map<Agenda>(entity));
                }).WithMessage("O horário indicado já está em uso para a sala");

            RuleFor(r => r.HorarioInicio)
                .LessThan(f => f.HorarioFim).WithMessage("Horário inicial deve ser menor do que horário final");

            RuleFor(r => r.SalaId)
                .NotEmpty().WithMessage("Campo [Sala] é obrigatório")
                .Must(p =>
                {
                    var sala = salaService.GetAsync(p).Result;
                    return sala != null;
                }).WithMessage(m => "A sala de Id " + m.SalaId + " não existe");
        }
    }
}

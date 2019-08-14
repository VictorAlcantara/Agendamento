using Agendamento.API.Contracts.V1.Requests;
using Agendamento.API.Domain.Interfaces.Services;
using FluentValidation;
using System.Linq;

namespace Agendamento.API.Validators
{
    public class UpdateSalaRequestValidator : AbstractValidator<UpdateSalaRequest>
    {
        public UpdateSalaRequestValidator(ISalaService salaService)
        {
            RuleFor(r => r.Id)
                .Must(p =>
                {
                    var sala = salaService.GetAsync(p).Result;
                    return sala != null;
                })
                .WithMessage(m => "Sala de Id " + m.Id + " não foi encontrada");

            RuleFor(r => r.Descricao)
                .Must(p => !string.IsNullOrEmpty(p.Trim())).WithMessage("Campo [descrição] não deve ser vazio")
                .Must((obj, p) =>
                {
                    var salas = salaService.GetByDescricaoAsync(obj.Descricao).Result.ToList();
                    if (salas.Where(w => w.Id != obj.Id).Count() > 0)
                        return false;
                    return true;
                })
                .WithMessage(m => "A descrição \'" + m.Descricao + "\' já existe");
        }
    }
}

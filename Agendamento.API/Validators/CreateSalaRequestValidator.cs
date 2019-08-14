using Agendamento.API.Contracts.V1.Requests;
using Agendamento.API.Domain.Interfaces.Services;
using FluentValidation;
using System.Linq;

namespace Agendamento.API.Validators
{
    public class CreateSalaRequestValidator : AbstractValidator<CreateSalaRequest>
    {
        public CreateSalaRequestValidator(ISalaService salaService)
        {
            RuleFor(r => r.Descricao)
                .Must(p => !string.IsNullOrEmpty(p.Trim())).WithMessage("Campo [descrição] não deve ser vazio")
                .Must((obj, p) =>
                {
                    var salas = salaService.GetByDescricaoAsync(obj.Descricao).Result;
                    if (salas.Count() > 0)
                        return false;
                    return true;
                })
                .WithMessage(m => "A descrição \'" + m.Descricao + "\' já existe");
        }
    }
}

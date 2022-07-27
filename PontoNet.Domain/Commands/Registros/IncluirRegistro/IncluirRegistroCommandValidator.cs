using FluentValidation;
using PontoNet.Domain.Extensions;

namespace PontoNet.Domain.Commands.Registros.IncluirRegistro
{
    public class IncluirRegistroCommandValidator : AbstractValidator<IncluirRegistroCommand>
    {
        public IncluirRegistroCommandValidator()
        {
            RuleFor(r => r.Hora)
                .Must(e => e.ValidateDateTime())
                .WithMessage("A hora informada é inválida");
        }
    }
}
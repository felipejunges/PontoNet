using FluentValidation;
using PontoNet.Domain.Extensions;

namespace PontoNet.Domain.Commands.Registros.AlterarRegistro
{
    public class AlterarRegistroCommandValidator : AbstractValidator<AlterarRegistroCommand>
    {
        public AlterarRegistroCommandValidator()
        {
            RuleFor(r => r.HoraInicial)
                .NotEmpty()
                .WithMessage("A hora inicial deve ser informada");

            RuleFor(r => r.HoraInicial)
                .Must(e => e.ValidateDateTime())
                .WithMessage("A hora inicial informada é inválida");

            RuleFor(r => r.HoraFinal)
                .Must(e => e.ValidateDateTime())
                .When(e => !string.IsNullOrEmpty(e.HoraFinal))
                .WithMessage("A hora final informada é inválida");
        }
    }
}
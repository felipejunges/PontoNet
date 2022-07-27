using MediatR;

namespace PontoNet.Domain.Commands.Registros.ExcluirRegistro
{
    public class ExcluirRegistroCommand : IRequest
    {
        public long Id { get; private set; }

        public ExcluirRegistroCommand(long id)
        {
            Id = id;
        }
    }
}
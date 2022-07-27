using MediatR;
using PontoNet.Domain.Entities;

namespace PontoNet.Domain.Commands.Registros.IncluirRegistro
{
    public class IncluirRegistroCommand : IRequest<Registro>
    {
        public DateTime Data { get; private set; }

        public string Hora { get; private set; }

        public IncluirRegistroCommand(DateTime data, string hora)
        {
            this.Data = data;
            this.Hora = hora;
        }
    }
}
using MediatR;
using PontoNet.Domain.Entities;

namespace PontoNet.Domain.Commands.Registros.RegistrarHorarioData
{
    public class RegistrarHorarioDataCommand : IRequest<Registro>
    {
        public DateTime Data { get; private set; }

        public RegistrarHorarioDataCommand(DateTime data)
        {
            Data = data;
        }
    }
}
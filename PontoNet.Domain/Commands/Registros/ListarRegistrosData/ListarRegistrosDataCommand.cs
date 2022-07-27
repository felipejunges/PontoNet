using MediatR;
using PontoNet.Domain.Entities;

namespace PontoNet.Domain.Commands.Registros.ListarRegistrosData
{
    public class ListarRegistrosDataCommand : IRequest<IEnumerable<Registro>>
    {
        public DateTime Data { get; private set; }

        public ListarRegistrosDataCommand(DateTime data)
        {
            Data = data;
        }
    }
}
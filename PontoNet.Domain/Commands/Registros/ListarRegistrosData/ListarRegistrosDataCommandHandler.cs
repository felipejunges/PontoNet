using MediatR;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces.Repositories;

namespace PontoNet.Domain.Commands.Registros.ListarRegistrosData
{
    public class ListarRegistrosDataCommandHandler : IRequestHandler<ListarRegistrosDataCommand, IEnumerable<Registro>>
    {
        private readonly IRegistroRepository _registroRepository;

        public ListarRegistrosDataCommandHandler(IRegistroRepository registroRepository)
        {
            _registroRepository = registroRepository;
        }

        public Task<IEnumerable<Registro>> Handle(ListarRegistrosDataCommand request, CancellationToken cancellationToken)
            => _registroRepository.ListarRegistrosDaDataAsync(request.Data);
    }
}
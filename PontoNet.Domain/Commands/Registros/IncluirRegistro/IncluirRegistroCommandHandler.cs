using MediatR;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces;
using PontoNet.Domain.Interfaces.Repositories;

namespace PontoNet.Domain.Commands.Registros.IncluirRegistro
{
    public class IncluirRegistroCommandHandler : IRequestHandler<IncluirRegistroCommand, Registro>
    {
        private readonly IRegistroRepository _registroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IncluirRegistroCommandHandler(IRegistroRepository registroRepository, IUnitOfWork unitOfWork)
        {
            _registroRepository = registroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Registro> Handle(IncluirRegistroCommand request, CancellationToken cancellationToken)
        {
            var registro = new Registro(
                request.Data,
                request.Hora
            );

            await _registroRepository.IncluirRegistroAsync(registro);

            await _unitOfWork.SaveChangesAsync();

            return registro;
        }
    }
}
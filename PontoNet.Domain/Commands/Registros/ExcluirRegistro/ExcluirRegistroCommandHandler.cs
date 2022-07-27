using MediatR;
using PontoNet.Domain.Interfaces;
using PontoNet.Domain.Interfaces.Repositories;
using PontoNet.Domain.Notifications.Interfaces;
using PontoNet.Domain.Notifications.Models;

namespace PontoNet.Domain.Commands.Registros.ExcluirRegistro
{
    public class ExcluirRegistroCommandHandler : IRequestHandler<ExcluirRegistroCommand>
    {
        private readonly IRegistroRepository _registroRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationContext _notificationContext;

        public ExcluirRegistroCommandHandler(IRegistroRepository registroRepository, IUnitOfWork unitOfWork, INotificationContext notificationContext)
        {
            _registroRepository = registroRepository;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        public async Task<Unit> Handle(ExcluirRegistroCommand request, CancellationToken cancellationToken)
        {
            var registro = await _registroRepository.ObterRegistroAsync(request.Id);

            if (registro == null)
            {
                _notificationContext.AddNotification("Registro não localizado", NotificationType.NotFound);
                return default;
            }

            await _registroRepository.ExcluirAsync(registro);
            await _unitOfWork.SaveChangesAsync();

            return default;
        }
    }
}
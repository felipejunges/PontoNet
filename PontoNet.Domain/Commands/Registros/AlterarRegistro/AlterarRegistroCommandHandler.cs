using MediatR;
using PontoNet.Domain.Interfaces;
using PontoNet.Domain.Interfaces.Repositories;
using PontoNet.Domain.Notifications.Interfaces;
using PontoNet.Domain.Notifications.Models;

namespace PontoNet.Domain.Commands.Registros.AlterarRegistro
{
    public class AlterarRegistroCommandHandler : IRequestHandler<AlterarRegistroCommand>
    {
        private readonly IRegistroRepository _registroRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationContext _notificationContext;

        public AlterarRegistroCommandHandler(IRegistroRepository registroRepository, IUnitOfWork unitOfWork, INotificationContext notificationContext)
        {
            _registroRepository = registroRepository;
            _unitOfWork = unitOfWork;
            _notificationContext = notificationContext;
        }

        public async Task<Unit> Handle(AlterarRegistroCommand request, CancellationToken cancellationToken)
        {
            var registro = await _registroRepository.ObterRegistroAsync(request.Id);

            if (registro == null)
            {
                _notificationContext.AddNotification("Registro não localizado", NotificationType.NotFound);
                return default;
            }

            registro.AlterarDados(
                request.Data,
                request.HoraInicial,
                request.HoraFinal);

            await _unitOfWork.SaveChangesAsync();

            return default;
        }
    }
}
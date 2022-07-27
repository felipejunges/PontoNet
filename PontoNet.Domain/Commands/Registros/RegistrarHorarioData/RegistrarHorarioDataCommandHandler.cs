using MediatR;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces;
using PontoNet.Domain.Interfaces.Repositories;

namespace PontoNet.Domain.Commands.Registros.RegistrarHorarioData
{
    public class RegistrarHorarioDataCommandHandler : IRequestHandler<RegistrarHorarioDataCommand, Registro>
    {
        private readonly IRegistroRepository _registroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarHorarioDataCommandHandler(IRegistroRepository registroRepository, IUnitOfWork unitOfWork)
        {
            _registroRepository = registroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Registro> Handle(RegistrarHorarioDataCommand request, CancellationToken cancellationToken)
        {
            var registrosData = await _registroRepository.ListarRegistrosDaDataAsync(request.Data);

            var registro = registrosData.OrderByDescending(r => r.HoraInicial).FirstOrDefault(r => r.HoraFinal == null);

            if (registro == null)
            {
                registro = new Registro(request.Data, DateTime.Now.TimeOfDay);
                
                await _registroRepository.IncluirRegistroAsync(registro);
            }
            else
            {
                registro.AlterarHoraFinal(DateTime.Now.TimeOfDay);
            }

            await _unitOfWork.SaveChangesAsync();

            return registro;
        }
    }
}
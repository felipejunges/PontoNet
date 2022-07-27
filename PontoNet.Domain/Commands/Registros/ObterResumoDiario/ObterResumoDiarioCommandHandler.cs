using MediatR;
using PontoNet.Domain.Interfaces.Repositories;
using PontoNet.Domain.ValueObjects;

namespace PontoNet.Domain.Commands.Registros.ObterResumoDiario
{
    public class ObterResumoDiarioCommandHandler : IRequestHandler<ObterResumoDiarioCommand, ResumoData>
    {
        private const double TEMPO_DIARIO = 8.5D; // TODO: parametrizar

        private readonly IFechamentoMesRepository _fechamentoMesRepository;
        private readonly IRegistroRepository _registroRepository;

        public ObterResumoDiarioCommandHandler(IFechamentoMesRepository fechamentoMesRepository, IRegistroRepository registroRepository)
        {
            _fechamentoMesRepository = fechamentoMesRepository;
            _registroRepository = registroRepository;
        }

        public async Task<ResumoData> Handle(ObterResumoDiarioCommand request, CancellationToken cancellationToken)
        {
            var mesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var fechamentoMes = await _fechamentoMesRepository.ObterFechamentoMesAsync(mesAtual);
            var registrosDaData = await _registroRepository.ListarRegistrosDaDataAsync(DateTime.Now.Date);

            var somaData = registrosDaData.Sum(r => r.Horas);
            var somaDataTs = TimeSpan.FromHours(somaData);

            var restanteData = TimeSpan.FromHours(TEMPO_DIARIO) - somaDataTs;
            var restanteMes = TimeSpan.FromHours(TEMPO_DIARIO - (fechamentoMes?.SaldoMes ?? 0)) - somaDataTs;
            var restanteFinal = TimeSpan.FromHours(TEMPO_DIARIO - (fechamentoMes?.SaldoFinal ?? 0)) - somaDataTs;

            return new ResumoData(mesAtual, restanteData, restanteMes, restanteFinal);
        }
    }
}
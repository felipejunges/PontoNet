using MediatR;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces;
using PontoNet.Domain.Interfaces.Repositories;

namespace PontoNet.Domain.Commands.FechamentosMes.CalcularFechamentoMes
{
    public class CalcularFechamentoMesCommandHandler : IRequestHandler<CalcularFechamentoMesCommand>
    {
        private readonly IFechamentoMesRepository _fechamentoMesRepository;
        private readonly IRegistroRepository _registroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CalcularFechamentoMesCommandHandler(IFechamentoMesRepository fechamentoMesRepository, IRegistroRepository registroRepository, IUnitOfWork unitOfWork)
        {
            _fechamentoMesRepository = fechamentoMesRepository;
            _registroRepository = registroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CalcularFechamentoMesCommand request, CancellationToken cancellationToken)
        {
            var registrosMes = await _registroRepository.ListarRegistrosDoMesAsync(request.Mes);
            
            var saldoMes = registrosMes.Sum(r => r.Horas);

            var saldoFinalMesAnterior = await ObterSaldoFechamentoMesAnterior(request.Mes);

            await IncluirOuAlterarFechamentoMes(request.Mes, saldoMes, saldoFinalMesAnterior);

            await _unitOfWork.SaveChangesAsync();

            return default;
        }

        private async Task IncluirOuAlterarFechamentoMes(DateTime mes, double saldoMes, double saldoFinalMesAnterior)
        {
            var fechamentoMes = await _fechamentoMesRepository.ObterFechamentoMesAsync(mes);

            if (fechamentoMes == null)
            {
                fechamentoMes = new FechamentoMes(mes, saldoFinalMesAnterior, saldoMes);

                await _fechamentoMesRepository.IncluirAsync(fechamentoMes);
            }
            else
                fechamentoMes.AlterarSaldos(saldoFinalMesAnterior, saldoMes);
        }

        private async Task<double> ObterSaldoFechamentoMesAnterior(DateTime mes)
        {
            var fechamentoMesAnterior = await _fechamentoMesRepository.ObterFechamentoMesAsync(mes.AddMonths(-1));
            return fechamentoMesAnterior?.SaldoFinal ?? 0D;
        }
    }
}
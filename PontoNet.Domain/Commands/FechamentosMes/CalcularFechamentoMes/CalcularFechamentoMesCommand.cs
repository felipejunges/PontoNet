using MediatR;

namespace PontoNet.Domain.Commands.FechamentosMes.CalcularFechamentoMes
{
    public class CalcularFechamentoMesCommand : IRequest
    {
        public DateTime Mes { get; private set; }

        public CalcularFechamentoMesCommand(DateTime mes)
        {
            Mes = mes;
        }
    }
}
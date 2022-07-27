namespace PontoNet.Domain.Entities
{
    public class FechamentoMes
    {
        public long Id { get; private set; }

        public DateTime Mes { get; private set; }

        public double SaldoInicial { get; private set; }

        public double SaldoMes { get; private set; }

        public double SaldoFinal => SaldoInicial + SaldoMes;

        public FechamentoMes(DateTime mes, double saldoInicial, double saldoMes)
        {
            Mes = mes;
            SaldoInicial = saldoInicial;
            SaldoMes = saldoMes;
        }

        public void AlterarSaldos(double saldoInicial, double saldoMes)
        {
            SaldoInicial = saldoInicial;
            SaldoMes = saldoMes;
        }
    }
}
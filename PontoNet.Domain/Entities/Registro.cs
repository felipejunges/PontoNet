using PontoNet.Domain.Extensions;

namespace PontoNet.Domain.Entities
{
    public class Registro
    {
        public long Id { get; private set; }

        public DateTime Data { get; private set; }

        public TimeSpan HoraInicial { get; private set; }

        public TimeSpan? HoraFinal { get; private set; }

        public double Horas => !HoraFinal.HasValue
                                    ? (DateTime.Now.TimeOfDay - HoraInicial).TotalHours
                                    : (HoraFinal.Value - HoraInicial).TotalHours + ((HoraFinal.Value < HoraInicial) ? 24D : 0D);

        public Registro(DateTime data, TimeSpan horaInicial)
        {
            this.Data = data;
            this.HoraInicial = horaInicial;
        }

        public Registro(DateTime data, string horaInicial)
            : this(data, horaInicial.ToDateTime())
        {
        }

        public Registro(DateTime data, string horaInicial, string horaFinal)
            : this(data, horaInicial)
        {
            this.HoraFinal = horaFinal.ToDateTime();
        }

        public void AlterarDados(DateTime data, string horaInicial, string? horaFinal)
        {
            Data = data;
            HoraInicial = horaInicial.ToDateTime();
            HoraFinal = horaFinal?.ToDateTime();
        }

        public void AlterarHoraFinal(TimeSpan horaFinal)
        {
            HoraFinal = horaFinal;
        }
    }
}
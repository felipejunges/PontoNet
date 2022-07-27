using MediatR;

namespace PontoNet.Domain.Commands.Registros.AlterarRegistro
{
    public class AlterarRegistroCommand : IRequest
    {
        public long Id { get; private set; }

        public DateTime Data { get; private set; }

        public string HoraInicial { get; private set; }

        public string HoraFinal { get; private set; }

        public AlterarRegistroCommand(long id, DateTime data, string horaInicial, string horaFinal)
        {
            this.Id = id;
            this.Data = data;
            this.HoraInicial = horaInicial;
            this.HoraFinal = horaFinal;
        }
    }
}
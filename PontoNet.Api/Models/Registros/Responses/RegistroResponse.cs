namespace PontoNet.Api.Models.Registros.Responses
{
    public class RegistroResponse
    {
        public long Id { get; init; }

        public DateTime Data { get; init; }

        public string? HoraInicial { get; init; }

        public string? HoraFinal { get; init; }
    }
}
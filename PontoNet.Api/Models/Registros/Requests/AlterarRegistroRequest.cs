namespace PontoNet.Api.Models.Registros.Requests
{
    public class AlterarRegistroRequest
    {
        public DateTime Data { get; set; }
        public string HoraInicial { get; set; } = null!;
        public string HoraFinal { get; set; } = null!;
    }
}
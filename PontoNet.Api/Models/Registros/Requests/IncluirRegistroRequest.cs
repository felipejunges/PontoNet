namespace PontoNet.Api.Models.Registros.Requests
{
    public class IncluirRegistroRequest
    {
        public DateTime Data { get; set; }
        public string Hora { get; set; } = null!;
    }
}
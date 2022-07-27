namespace PontoNet.Api.Models.Registros.Responses
{
    public class ResumoDiarioResponse
    {
        public DateTime Data { get; set; }

        public string? RestanteData { get; set; }

        public string? RestanteMes { get; set; }

        public string? RestanteFinal { get; set; }
    }
}
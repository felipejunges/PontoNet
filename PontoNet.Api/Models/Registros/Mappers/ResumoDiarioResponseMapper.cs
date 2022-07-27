using PontoNet.Api.Models.Registros.Responses;
using PontoNet.Domain.Extensions;
using PontoNet.Domain.ValueObjects;

namespace PontoNet.Api.Models.Registros.Mappers
{
    public static class ResumoDiarioResponseMapper
    {
        public static ResumoDiarioResponse Map(ResumoData resumoData)
        {
            return new ResumoDiarioResponse()
            {
                Data = resumoData.Data,
                RestanteData = resumoData.RestanteData.ToTimeString(),
                RestanteMes = resumoData.RestanteMes.ToTimeString(),
                RestanteFinal = resumoData.RestanteFinal.ToTimeString()
            };
        }
    }
}
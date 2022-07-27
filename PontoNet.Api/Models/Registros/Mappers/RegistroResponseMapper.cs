using PontoNet.Api.Models.Registros.Responses;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Extensions;

namespace PontoNet.Api.Models.Registros.Mappers
{
    public static class RegistroResponseMapper
    {
        public static IEnumerable<RegistroResponse> Map(IEnumerable<Registro> registros)
        {
            if (registros.Count() == 0)
                return Enumerable.Empty<RegistroResponse>();

            return registros.Select(Map);
        }

        public static RegistroResponse Map(Registro registro)
        {
            return new RegistroResponse()
            {
                Id = registro.Id,
                Data = registro.Data,
                HoraInicial = registro.HoraInicial.ToTimeString(),
                HoraFinal = registro.HoraFinal.ToTimeString()
            };
        }
    }
}
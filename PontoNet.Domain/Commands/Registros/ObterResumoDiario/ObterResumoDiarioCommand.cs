using MediatR;
using PontoNet.Domain.ValueObjects;

namespace PontoNet.Domain.Commands.Registros.ObterResumoDiario
{
    public class ObterResumoDiarioCommand : IRequest<ResumoData>
    {
    }
}
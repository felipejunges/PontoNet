using PontoNet.Domain.Entities;

namespace PontoNet.Domain.Interfaces.Repositories
{
    public interface IFechamentoMesRepository
    {
        Task<FechamentoMes?> ObterFechamentoMesAsync(DateTime mes);
        Task IncluirAsync(FechamentoMes fechamentoMes);
    }
}
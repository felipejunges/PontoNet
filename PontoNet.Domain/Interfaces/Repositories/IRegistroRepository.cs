using PontoNet.Domain.Entities;

namespace PontoNet.Domain.Interfaces.Repositories
{
    public interface IRegistroRepository
    {
        Task<Registro?> ObterRegistroAsync(long id);
        Task<IEnumerable<Registro>> ListarRegistrosDaDataAsync(DateTime data);
        Task<IEnumerable<Registro>> ListarRegistrosDoMesAsync(DateTime mes);
        Task IncluirRegistroAsync(Registro registro);
        Task ExcluirAsync(Registro registro);
    }
}
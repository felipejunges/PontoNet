using Microsoft.EntityFrameworkCore;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces.Repositories;
using PontoNet.Infra.Context;

namespace PontoNet.Infra.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {
        public PontoContext _context { get; }

        public RegistroRepository(PontoContext context)
        {
            _context = context;
        }

        public async Task<Registro?> ObterRegistroAsync(long id)
        {
            return await _context.Registros
                        .Where(r => r.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Registro>> ListarRegistrosDaDataAsync(DateTime data)
        {
            return await _context.Registros
                        .Where(r => r.Data == data)
                        .OrderBy(r => r.HoraInicial)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Registro>> ListarRegistrosDoMesAsync(DateTime mes)
        {
            var dataInicial = mes;
            var dataFinal = mes.AddDays(DateTime.DaysInMonth(mes.Year, mes.Month)).AddDays(-1);

            return await _context.Registros
                        .Where(r =>
                            r.Data >= dataInicial
                            && r.Data <= dataFinal
                        )
                        .OrderBy(r => r.HoraInicial)
                        .ToListAsync();
        }

        public async Task IncluirRegistroAsync(Registro registro)
        {
            await _context.Registros.AddAsync(registro);
        }

        public async Task ExcluirAsync(Registro registro)
        {
            await Task.Run(() => _context.Registros.Remove(registro));
        }
    }
}
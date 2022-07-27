using Microsoft.EntityFrameworkCore;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces.Repositories;
using PontoNet.Infra.Context;

namespace PontoNet.Infra.Repositories
{
    public class FechamentoMesRepository : IFechamentoMesRepository
    {
        public PontoContext _context { get; }

        public FechamentoMesRepository(PontoContext context)
        {
            _context = context;
        }

        public Task<FechamentoMes?> ObterFechamentoMesAsync(DateTime mes)
        {
            return _context.FechamentosMes.FirstOrDefaultAsync(f => f.Mes == mes);
        }

        public async Task IncluirAsync(FechamentoMes fechamentoMes)
        {
            await _context.FechamentosMes.AddAsync(fechamentoMes);
        }
    }
}
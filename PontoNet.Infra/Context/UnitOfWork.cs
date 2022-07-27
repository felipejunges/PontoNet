using PontoNet.Domain.Interfaces;

namespace PontoNet.Infra.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PontoContext _context;

        public UnitOfWork(PontoContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await Task.Run(() => _context.Database.CommitTransaction());
        }

        public async Task RollbackTransactionAsync()
        {
            await Task.Run(() => _context.Database.RollbackTransaction());
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
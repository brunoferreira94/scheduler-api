using Scheduler.Domain.Interfaces.UoW;
using Scheduler.Infra.Data.Context;

namespace ProductionOrder.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchedulerContext _context;

        public UnitOfWork(SchedulerContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            try
            {
                int _commited = _context.SaveChanges();

                return _commited > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                int _commited = await _context.SaveChangesAsync();

                return _commited > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
            => _context.Dispose();
    }
}
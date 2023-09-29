using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Domain.Entities;
using Scheduler.Infra.Data.Context;

namespace Scheduler.Infra.Data.Repositories
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(SchedulerContext context) : base(context)
        {
        }
    }
}
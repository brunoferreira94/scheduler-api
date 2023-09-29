using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Domain.Entities;
using Scheduler.Infra.Data.Context;

namespace Scheduler.Infra.Data.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(SchedulerContext context) : base(context)
        {
        }
    }
}
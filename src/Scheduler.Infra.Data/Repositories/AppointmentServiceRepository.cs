using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Domain.Entities;
using Scheduler.Infra.Data.Context;

namespace Scheduler.Infra.Data.Repositories
{
    public class AppointmentServiceRepository : Repository<AppointmentService>, IAppointmentServiceRepository
    {
        public AppointmentServiceRepository(SchedulerContext context) : base(context)
        {
        }
    }
}
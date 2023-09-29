using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Domain.Entities;
using Scheduler.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Scheduler.Infra.Data.Repositories
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(SchedulerContext context) : base(context)
        {
        }
    }
}
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.Interfaces
{
    public interface IAppointmentService : IService<AppointmentViewModel>
    {
        List<AppointmentViewModel> GetAppointmentsWithin24Hours(DateTime referenceDateTime);
    }
}
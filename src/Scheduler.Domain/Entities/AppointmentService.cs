namespace Scheduler.Domain.Entities
{
    public class AppointmentService
    {
        public AppointmentService()
        {
        }

        public AppointmentService(Guid serviceId)
        {
            ServiceId = serviceId;
        }

        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
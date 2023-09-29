namespace Scheduler.Domain.Entities
{
    public class Appointment : Entity
    {
        #region Public Constructors

        public Appointment()
        { }

        public Appointment(DateTime dateTime, Guid clientId)
        {
            ScheduledDate = dateTime;
            ClientId = clientId;
        }

        #endregion Public Constructors

        #region Public Properties

        public DateTime ScheduledDate { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; } = default!;
        public ICollection<AppointmentService> AppointmentServices { get; set; }

        #endregion Public Properties
    }
}
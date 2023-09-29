namespace Scheduler.Domain.Entities
{
    public class Service : Entity
    {
        public Service()
        {
        }

        public Service(Guid id, string name = "")
        {
            Id = id;
            Name = name;
        }

        public string Name { get; set; }
        public ICollection<AppointmentService> AppointmentServices { get; set; }
    }
}
namespace Scheduler.Domain.Entities
{
    public class Client : Entity
    {
        public Client()
        { }

        public Client(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
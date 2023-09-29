namespace Scheduler.Application.ViewModels
{
    public class ClientViewModel : EntityViewModel
    {
        public ClientViewModel()
        {
        }

        public ClientViewModel(string email)
        {
            Email = email;
        }

        public ClientViewModel(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<AppointmentViewModel> Appointments { get; set; } = new List<AppointmentViewModel>();
    }
}
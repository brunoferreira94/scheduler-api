namespace Scheduler.Application.ViewModels
{
    public class ServiceViewModel : EntityViewModel
    {
        public ServiceViewModel(Guid id, string? name)
        {
            Id = id;
            Name = name;

        }

        public string? Name { get; set; }
    }
}
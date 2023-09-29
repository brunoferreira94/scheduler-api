using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Application.ViewModels
{
    public class AppointmentViewModel : EntityViewModel
    {
        public AppointmentViewModel()
        {
        }

        public AppointmentViewModel(DateTime scheduledDate, string email)
        {
            ScheduledDate = scheduledDate;
            Client = new ClientViewModel(email);
        }

        public AppointmentViewModel(Guid id, DateTime scheduledDate, Guid clientId, ClientViewModel? client, IEnumerable<ServiceViewModel> services)
        {
            Id = id;
            ScheduledDate = scheduledDate;
            ClientId = clientId;
            Client = client;
            Services = services;
        }
        #region Public Properties

        public DateTime ScheduledDate { get; set; }
        public Guid ClientId { get; set; }
        public ClientViewModel? Client { get; set; }
        public IEnumerable<ServiceViewModel> Services { get; set; }

        #endregion Public Properties
    }
}
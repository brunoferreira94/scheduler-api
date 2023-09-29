using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scheduler.Application.Interfaces;
using Scheduler.Application.ViewModels;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.Services
{
    public class ReminderNotificationService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private const int CheckIntervalMinutes = 60;

        public ReminderNotificationService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _provider.CreateScope())
                {
                    IAppointmentService appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();
                    IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    DateTime now = DateTime.Now;
                    List<AppointmentViewModel> appointments = appointmentService.GetAppointmentsWithin24Hours(now);

                    foreach (AppointmentViewModel appointment in appointments)
                    {
                        TimeSpan timeUntilAppointment = appointment.ScheduledDate - now;
                        if (timeUntilAppointment.TotalHours <= 24)
                        {
                            string emailContent = $"Lembrete: Seu agendamento está marcado para {appointment.ScheduledDate}.";
                            emailService.SendEmail(appointment.Client!.Email, "Lembrete de Agendamento", emailContent);
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(CheckIntervalMinutes), stoppingToken);
            }
        }
    }
}
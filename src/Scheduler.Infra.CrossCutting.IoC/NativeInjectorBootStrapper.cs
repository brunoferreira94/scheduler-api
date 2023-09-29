using Scheduler.Application.Interfaces;
using Scheduler.Application.Services;
using Scheduler.Domain.Interfaces.Repositories;
using Scheduler.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ProductionOrder.Infra.Data.UoW;
using Scheduler.Domain.Interfaces.UoW;
using Scheduler.Domain.Settings;

namespace Scheduler.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        #region Public Methods

        public static void RegisterServices(IServiceCollection services, EmailSettings emailSettings)
        {
            string smtpServer = emailSettings.SmtpServer;
            int smtpPort = int.Parse(emailSettings.SmtpPort);
            string smtpUsername = emailSettings.SmtpUsername;
            string smtpPassword = emailSettings.SmtpPassword;

            #region Singleton

            #region Services

            services.AddSingleton<IEmailService>(new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword));

            #endregion Services

            #endregion Singleton

            #region Scoped

            #region Infra - Data uow

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion Infra - Data uow

            #region Repositories

            services.AddScoped<IAppointmentServiceRepository, AppointmentServiceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            #endregion Repositories

            #region Services

            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IServiceService, ServiceService>();

            #endregion Services

            #endregion Scoped

            #region Hosted Service

            services.AddHostedService<ReminderNotificationService>();

            #endregion Hosted Service
        }

        #endregion Public Methods
    }
}
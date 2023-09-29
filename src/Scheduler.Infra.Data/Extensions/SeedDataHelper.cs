using Microsoft.EntityFrameworkCore;
using Scheduler.Domain.Entities;

namespace Scheduler.Infra.Data.Extensions
{
    public static class SeedDataHelper
    {
        #region Public Methods

        /// <summary>
        /// Utilize esse método para inserir informações no BD automaticamente através do comando 'Update-Database'
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            #region Client

            builder.Entity<Client>()
                   .HasData(new Client
                   {
                       Id = new Guid("5748500f-98f9-460b-8aaf-a39591065aec"),
                       CreatedDate = DateTime.Now,
                       ModifiedDate = null,
                       IsDeleted = false,
                       Email = "email@email.com",
                       Name = "João da Silva"
                   });

            #endregion Client

            #region Appointment

            builder.Entity<Appointment>()
                   .HasData(new List<Appointment>
                   {
                       new Appointment
                       {
                           Id = new Guid("f3071fb3-aafc-4f06-9107-b29281354266"),
                           CreatedDate = DateTime.Now,
                           ModifiedDate = null,
                           IsDeleted = false,
                           ClientId = new Guid("5748500f-98f9-460b-8aaf-a39591065aec"),
                           ScheduledDate = DateTime.Parse("2023/09/29 08:00"),
                       },
                       new Appointment
                       {
                           Id = new Guid("a5a8621f-0930-4199-b0a8-e43ca5d89b74"),
                           CreatedDate = DateTime.Now,
                           ModifiedDate = null,
                           IsDeleted = false,
                           ClientId = new Guid("5748500f-98f9-460b-8aaf-a39591065aec"),
                           ScheduledDate = DateTime.Parse("2023/10/15 12:00"),
                       },
                       new Appointment
                       {
                           Id = new Guid("4e7fe69c-7e2e-4fb8-bc1a-4fe91b38d597"),
                           CreatedDate = DateTime.Now,
                           ModifiedDate = null,
                           IsDeleted = false,
                           ClientId = new Guid("5748500f-98f9-460b-8aaf-a39591065aec"),
                           ScheduledDate = DateTime.Parse("2023/10/30 08:00"),
                       }
                   });

            #endregion Appointment

            #region Service

            builder.Entity<Service>()
                   .HasData(new Service
                   {
                       Id = new Guid("820d24da-2abf-4661-97a8-529ce54cc378"),
                       CreatedDate = DateTime.Now,
                       ModifiedDate = null,
                       IsDeleted = false,
                       Name = "Corte"
                   });

            builder.Entity<Service>()
                   .HasData(new Service
                   {
                       Id = new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812"),
                       CreatedDate = DateTime.Now,
                       ModifiedDate = null,
                       IsDeleted = false,
                       Name = "Barba"
                   });

            builder.Entity<Service>()
                   .HasData(new Service
                   {
                       Id = new Guid("18991fb6-827d-4a07-8a8c-33cc7f38dba4"),
                       CreatedDate = DateTime.Now,
                       ModifiedDate = null,
                       IsDeleted = false,
                       Name = "Luzes"
                   });

            #endregion Service

            #region AppointmentService

            builder.Entity<AppointmentService>()
                   .HasData(new AppointmentService
                   {
                       AppointmentId = new Guid("f3071fb3-aafc-4f06-9107-b29281354266"),
                       ServiceId = new Guid("820d24da-2abf-4661-97a8-529ce54cc378"),
                   });

            builder.Entity<AppointmentService>()
                   .HasData(new AppointmentService
                   {
                       AppointmentId = new Guid("f3071fb3-aafc-4f06-9107-b29281354266"),
                       ServiceId = new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812"),
                   });

            builder.Entity<AppointmentService>()
                   .HasData(new AppointmentService
                   {
                       AppointmentId = new Guid("a5a8621f-0930-4199-b0a8-e43ca5d89b74"),
                       ServiceId = new Guid("820d24da-2abf-4661-97a8-529ce54cc378"),
                   });

            builder.Entity<AppointmentService>()
                   .HasData(new AppointmentService
                   {
                       AppointmentId = new Guid("a5a8621f-0930-4199-b0a8-e43ca5d89b74"),
                       ServiceId = new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812"),
                   });

            builder.Entity<AppointmentService>()
                   .HasData(new AppointmentService
                   {
                       AppointmentId = new Guid("4e7fe69c-7e2e-4fb8-bc1a-4fe91b38d597"),
                       ServiceId = new Guid("18991fb6-827d-4a07-8a8c-33cc7f38dba4"),
                   });

            builder.Entity<AppointmentService>()
                   .HasData(new AppointmentService
                   {
                       AppointmentId = new Guid("4e7fe69c-7e2e-4fb8-bc1a-4fe91b38d597"),
                       ServiceId = new Guid("8f36d07c-b67c-4623-a5b3-a03eb9da6812"),
                   });

            #endregion AppointmentService

            return builder;
        }

        #endregion Public Methods
    }
}
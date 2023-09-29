using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Entities;

namespace Scheduler.Infra.Data.Mappings
{
    public class AppointmentServiceMap : IEntityTypeConfiguration<AppointmentService>
    {
        public void Configure(EntityTypeBuilder<AppointmentService> builder)
        {
            builder.HasKey(appointmentService => new { appointmentService.AppointmentId, appointmentService.ServiceId });

            builder.HasOne(appointmentService => appointmentService.Appointment)
                   .WithMany(appointment => appointment.AppointmentServices)
                   .HasForeignKey(appointmentService => appointmentService.AppointmentId);

            builder.HasOne(appointmentService => appointmentService.Service)
                   .WithMany(service => service.AppointmentServices)
                   .HasForeignKey(appointmentService => appointmentService.ServiceId);
        }
    }
}
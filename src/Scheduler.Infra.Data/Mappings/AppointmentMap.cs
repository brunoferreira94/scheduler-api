using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Entities;

namespace Scheduler.Infra.Data.Mappings
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(appointment => appointment.Id);
            builder.Property(appointment => appointment.ScheduledDate).IsRequired();
            builder.HasOne(appointment => appointment.Client)
                   .WithMany(client => client.Appointments)
                   .HasForeignKey(appointment => appointment.ClientId)
                   .IsRequired();
        }
    }
}
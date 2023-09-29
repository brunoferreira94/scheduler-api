using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduler.Domain.Entities;

namespace Scheduler.Infra.Data.Mappings
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(client => client.Id);
            builder.Property(client => client.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(client => client.Email)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.HasIndex(client => client.Email).IsUnique();
        }
    }
}
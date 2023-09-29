using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Scheduler.Domain.Entities;
using Scheduler.Infra.Data.Extensions;

namespace Scheduler.Infra.Data.Context
{
    public class SchedulerContext : DbContext
    {
        #region Public Constructors

        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Client> Clients { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulerContext).Assembly);
            modelBuilder.ApplyGlobalStandards();
            modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(wh => wh.ClrType.BaseType == typeof(Entity)))
            {
                /// <summary>
                /// Cria uma expressão para remover deletados
                /// "entity => EF.Property<bool>(entity, "IsDeleted") == false"
                /// </summary>
                ParameterExpression parameter = Expression.Parameter(entityType.ClrType);
                MethodInfo propertyMethodInfo = typeof(EF).GetMethod("Property")!.MakeGenericMethod(typeof(bool));
                MethodCallExpression isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                LambdaExpression lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;

            IConfigurationRoot _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                          .AddJsonFile(path: "appsettings.json",
                                                                                       optional: true)
                                                                          .AddJsonFile(path: $"appsettings.{_environment}.json",
                                                                                       optional: true,
                                                                                       reloadOnChange: true)
                                                                          .AddEnvironmentVariables()
                                                                          .Build();

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(SchedulerContext)))
                              .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            optionsBuilder.EnableSensitiveDataLogging();
        }

        #endregion Protected Methods

        #region Public Methods

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess: acceptAllChangesOnSuccess,
                                         cancellationToken: cancellationToken);
        }

        #endregion Public Methods

        #region Private Methods

        private void OnBeforeSaving()
        {
            ChangeTracker.Entries()
                         .ToList()
                         .ForEach(entry =>
                         {
                             if (entry.Entity is Entity trackableEntity)
                             {
                                 if (entry.State == EntityState.Added)
                                 {
                                     trackableEntity.CreatedDate = DateTime.Now;
                                     trackableEntity.IsDeleted = false;
                                 }
                                 else if (entry.State == EntityState.Modified)
                                     trackableEntity.ModifiedDate = DateTime.Now;
                             }
                         });
        }

        #endregion Private Methods
    }
}
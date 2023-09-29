using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System;

namespace Scheduler.Infra.Data.Extensions
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/april/data-points-ef-core-in-a-docker-containerized-app
    /// </summary>
    public static class MigrationExtensions
    {
        #region Metodos Publicos

        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider _services = scope.ServiceProvider;

                try
                {
                    T _context = _services.GetRequiredService<T>();

                    _context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    ILogger<Program> _logger = _services.GetRequiredService<ILogger<Program>>();

                    _logger.LogError(exception: ex,
                                     message: "Um erro ocorreu enquanto o banco de dados era criado.");
                }
            }

            return host;
        }

        #endregion Metodos Publicos
    }
}
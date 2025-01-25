namespace EmployeeAngular.Server.Infrastrcture
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbcontext = services.ServiceProvider.GetService<ApplicationDbContext>();
            dbcontext.Database.Migrate();
        }
    }
}

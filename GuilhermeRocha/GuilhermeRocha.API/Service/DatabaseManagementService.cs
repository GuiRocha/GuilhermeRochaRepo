using GuilhermeRocha.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GuilhermeRocha.API.Service
{
    public static class DatabaseManagementService
    {

        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<GuilhermeContext>().Database.Migrate();
            }
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Data;

namespace UltraBet.Infrastructure.DbExtension
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app
                                         .ApplicationServices
                                         .GetRequiredService<IServiceScopeFactory>()
                                         .CreateScope())
            {
                serviceScope
                    .ServiceProvider
                    .GetService<BetPlusDbContext>()
                    .Database
                    .Migrate();
            }

            return app;
        }
    }
}

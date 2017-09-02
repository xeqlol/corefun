using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CoreFun.Data;

namespace CoreFun
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build();

            using (var scope = host.Services.CreateScope())
            {
                var servives = scope.ServiceProvider;

                try
                {
                    var context = servives.GetRequiredService<SchoolContext>();
                    DBInitializer.Initialize(context);
                }
                catch (Exception e)
                {
                    var logger = servives.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occureed while seeding the database. OMG WE ALL GONNA DIE, AAAAAAAAAAAAAAAAAAAAAAAAAAAA...");
                }
            }

            host.Run(); // let's go bois
        }
    }
}

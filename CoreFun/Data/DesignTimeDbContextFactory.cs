using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreFun.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SchoolContext>
    {
        /*
         * If you see that when trying to make migration:
         * 
         *      Unable to create an object of type 'SchoolContext'. 
         *      Add an implementation of 'IDesignTimeDbContextFactory<SchoolContext>' to the project, 
         *      or see https://go.microsoft.com/fwlink/?linkid=851728 for 
         *      additional patterns supported at design time.
         *      
         * then you just need to implement something like that class below.
         */
        public SchoolContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<SchoolContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new SchoolContext(builder.Options);
        }
    }
}

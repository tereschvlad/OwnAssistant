using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
using OwnAssistantCommon.Services;

namespace OwnAssistatntTest
{
    public class Utils
    {
        private static IServiceProvider Provider()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DataContext>(sql => sql.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OwnAssistant;Integrated Security=True"));
            services.AddScoped<IDbRepository, DbRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerTaskService, CustomerTaskService>();
            services.AddLogging();

            return services.BuildServiceProvider();
        }

        public static T GetRequiredService<T>() where T : class
        {
            return Provider().GetRequiredService<T>();
        }
    }
}

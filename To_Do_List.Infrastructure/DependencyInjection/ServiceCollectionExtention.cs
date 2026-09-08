using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using To_Do_List.Domain.Interfaces;
using To_Do_List.Infrastructure.Data;
using To_Do_List.Infrastructure.Repositories;

namespace To_Do_List.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITaskRepository, TaskRepository>();


            return services;
        }
        
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace MyCampus.Service.Configurations
{
    public static class MyCampusServiceExtension
    {
        public static void AddMyCampusServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MyCampusServiceExtension).Assembly);
        }
    }
}

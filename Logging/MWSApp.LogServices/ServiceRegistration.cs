

namespace MWSApp.LogServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IRepository<CompanyLog>), typeof(Repository<CompanyLog>));
            return services;
        }
    }
}

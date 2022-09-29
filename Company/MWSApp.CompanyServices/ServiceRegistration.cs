

namespace MWSApp.CompanyServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IRepository<Company>), typeof(Repository<Company>));
            return services;
        }
    }
}

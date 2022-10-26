



using Microsoft.Extensions.Configuration;
using MWSApp.CommonInterfaces.Redis;

namespace MWSApp.CompanyServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IRepository<Company>), typeof(Repository<Company>));
            services.AddScoped(typeof(IRepository<CompanyUser>), typeof(Repository<CompanyUser>));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IRedisConnectionFactory>(new RedisConnectionFactory(configuration["RedisSettings:Url"]));
            services.AddSingleton<ICacheService, RedisCacheService>();
            return services;
        }
    }
}



namespace MWSApp.LogContexts
{
    public static class DbContextServiceRegistration
    {
        public static IServiceCollection RegisterDbContextServices (this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<LogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.Configure<RabbitMQSetting>(c=> configuration.GetSection("RabbitMQSetting"));
            return services;
        }

        public static void MigrateDatabase(this IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<LogDbContext>();
            dataContext.Database.Migrate();
        }
    }
}

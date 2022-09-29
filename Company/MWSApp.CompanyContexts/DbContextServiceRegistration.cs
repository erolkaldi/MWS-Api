

namespace MWSApp.CompanyContexts
{
    public static class DbContextServiceRegistration
    {
        public static IServiceCollection RegisterDbContextServices (this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });
            //services.AddMassTransit(x => {
            //    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
            //    {
            //        config.Host(new Uri(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUri").Value), h =>
            //        {
            //            h.Username(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUser").Value);
            //            h.Password(configuration.GetSection("RabbitMQSetting").GetSection("RabbitPassword").Value);
            //        });
            //    }));
            //});
            //services.AddOptions<MassTransitHostOptions>()
            //.Configure(options =>
            //{
            //    options.WaitUntilStarted = true;
        
            //    options.StartTimeout = TimeSpan.FromSeconds(10);
        
            //    options.StopTimeout = TimeSpan.FromSeconds(30);
        
            //});
            services.Configure<RabbitMQSetting>(c=> configuration.GetSection("RabbitMQSetting"));
            return services;
        }

        public static void MigrateDatabase(this IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
            dataContext.Database.Migrate();
        }
    }
}

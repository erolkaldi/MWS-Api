





namespace MWSApp.LogServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IRepository<CompanyLog>), typeof(Repository<CompanyLog>));
            services.AddMassTransit(config =>
            {

                config.AddConsumer<CreateLogConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {

          


                    cfg.Host(configuration["RabbitMQSetting:RabbitUri"]);

                    cfg.ReceiveEndpoint(configuration["RabbitMQSetting:RabbitQueue"], c =>
                    {
                        c.ConfigureConsumer<CreateLogConsumer>(ctx);
                    });

                });
            });

            return services;
        }
    }
}

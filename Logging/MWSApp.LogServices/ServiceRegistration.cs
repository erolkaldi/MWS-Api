
namespace MWSApp.LogServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IRepository<CompanyLog>), typeof(Repository<CompanyLog>));
            services.AddScoped(typeof(IRepository<MailLog>), typeof(Repository<MailLog>));
            services.AddMassTransit(config =>
            {

                config.AddConsumer<CreateLogConsumer>();
                config.AddConsumer<MailLogConsumer>();

                config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUri").Value), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUser").Value);
                        h.Password(configuration.GetSection("RabbitMQSetting").GetSection("RabbitPassword").Value);
                    });
                    config.ReceiveEndpoint(configuration["RabbitMQSetting:RabbitQueue"], ep =>
                    {
                        ep.ConfigureConsumer<CreateLogConsumer>(provider);
                    });
                    config.ReceiveEndpoint(configuration["RabbitMQSetting:MailLogQueue"], ep =>
                    {
                        ep.ConfigureConsumer<MailLogConsumer>(provider);
                    });
                }));

            });

            return services;
        }
    }
}

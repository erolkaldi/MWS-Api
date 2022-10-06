

namespace MWSApp.MailServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMassTransit(x =>
            {
                x.AddConsumer<SendMailConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUri").Value), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUser").Value);
                        h.Password(configuration.GetSection("RabbitMQSetting").GetSection("RabbitPassword").Value);
                    });
                    config.ReceiveEndpoint(configuration["RabbitMQSetting:MailQueue"], ep =>
                    {
                        ep.ConfigureConsumer<SendMailConsumer>(provider);
                    });
                }));
                
            });
            services.Configure<RabbitMQSetting>(c => configuration.GetSection("RabbitMQSetting"));
            services.Configure<SmtpSetting>(c => configuration.GetSection("Smtp"));
            return services;
        }
    }
}

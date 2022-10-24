



namespace MWSApp.IdentityServices
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = true,
            //        ValidateAudience = false,
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            //    };
            //});
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUri").Value), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMQSetting").GetSection("RabbitUser").Value);
                        h.Password(configuration.GetSection("RabbitMQSetting").GetSection("RabbitPassword").Value);
                    });
                }));
            });
            services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;

                options.StartTimeout = TimeSpan.FromSeconds(10);

                options.StopTimeout = TimeSpan.FromSeconds(30);

            });
            services.Configure<RabbitMQSetting>(c => configuration.GetSection("RabbitMQSetting"));
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}

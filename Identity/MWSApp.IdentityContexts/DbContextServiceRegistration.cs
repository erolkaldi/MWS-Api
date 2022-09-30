

namespace MWSApp.IdentityContexts
{
    public static class DbContextServiceRegistration
    {
        public static IServiceCollection RegisterDbContextServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            return services;
        }
        public static void MigrateDatabase(this IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            dataContext.Database.Migrate();
        }
    }
}

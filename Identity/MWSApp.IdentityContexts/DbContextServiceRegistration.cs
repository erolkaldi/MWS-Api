

namespace MWSApp.IdentityContexts
{
    public static class DbContextServiceRegistration
    {
        public static IServiceCollection RegisterDbContextServices(this IServiceCollection services,IConfiguration configuration)
        {
           
            return services;
        }
        public static void MigrateDatabase(this IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            dataContext.Database.Migrate();
        }
    }
}

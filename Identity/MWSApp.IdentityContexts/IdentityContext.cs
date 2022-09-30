

namespace MWSApp.IdentityContexts
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}

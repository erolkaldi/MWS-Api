



namespace MWSApp.LogContexts
{
    public class LogDbContext:DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(6000);
            base.ChangeTracker.AutoDetectChangesEnabled = true;
        }
        public DbSet<CompanyLog> CompanyLogs { get; set; }
        public DbSet<MailLog> MailLogs { get; set; }
        public int Execute(string query, object parameters)
        {
            return Database.GetDbConnection().Execute(query, parameters);
        }
        public List<T> SQLQuery<T>(string query, object parameters)
        {

            return Database.GetDbConnection().Query<T>(query, param: parameters).ToList();
        }
        public List<T> SQLQuery<T>(string query)
        {
            return Database.GetDbConnection().Query<T>(query).ToList();
        }
    }
}

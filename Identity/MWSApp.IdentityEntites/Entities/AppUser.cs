



namespace MWSApp.IdentityEntites.Entities
{
    public class AppUser :IdentityUser
    {
        [MaxLength(150)]
        public string FullName { get; set; } = "";
    }
}

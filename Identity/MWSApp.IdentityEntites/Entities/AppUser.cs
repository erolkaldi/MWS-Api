



namespace MWSApp.IdentityEntites.Entities
{
    public class AppUser :IdentityUser
    {
        public AppUser()
        {
            RefreshTokens = new List<RefreshToken>();
        }
        [MaxLength(150)]
        public string FullName { get; set; } = "";
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}

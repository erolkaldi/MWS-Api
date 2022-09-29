
namespace MWSApp.CompanyEntities.Entities
{
    public class Company:BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";

        public Company()
        {

        }
        public Company(string name,string email)
        {
            Name = name;
            Email = email;
            Id = Guid.NewGuid();
        }
    }
}

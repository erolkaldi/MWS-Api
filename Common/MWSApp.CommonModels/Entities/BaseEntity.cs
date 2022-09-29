

using System.ComponentModel.DataAnnotations;

namespace MWSApp.CommonModels.Entities
{
    public class BaseEntity :MainBase
    {
        
        public DateTime CreateDate { get; set; }
        [MaxLength(20)]
        public string CreateUser { get; set; } = "";
        public bool Deleted { get; set; }
    }
}

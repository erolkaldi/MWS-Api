
using System.ComponentModel.DataAnnotations;

namespace MWSApp.CommonModels.Entities
{
    public class MainBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}

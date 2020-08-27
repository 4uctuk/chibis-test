using System.ComponentModel.DataAnnotations;

namespace ChibisTest.Features.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

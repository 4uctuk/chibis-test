using System.ComponentModel.DataAnnotations;

namespace ChibisTest.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}

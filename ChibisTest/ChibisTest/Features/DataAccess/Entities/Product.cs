using System.Collections.ObjectModel;

namespace ChibisTest.Features.DataAccess.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public virtual Collection<CartItem> CartItems { get; set; }
    }
}

using System.Collections.ObjectModel;

namespace ChibisTest.DataAccess.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public virtual Collection<CartItem> CartItems { get; set; }
    }
}

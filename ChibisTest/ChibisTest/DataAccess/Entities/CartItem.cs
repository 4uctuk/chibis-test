using System.Text.Json.Serialization;

namespace ChibisTest.DataAccess.Entities
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public bool ForBonusPoints { get; set; }

        [JsonIgnore]
        public virtual Cart Cart { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}

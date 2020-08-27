using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ChibisTest.DataAccess.Entities
{
    public class Cart : BaseEntity
    {
        public Guid CartId { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual Collection<CartItem> CartItems { get; set; } = new Collection<CartItem>();
    }
}

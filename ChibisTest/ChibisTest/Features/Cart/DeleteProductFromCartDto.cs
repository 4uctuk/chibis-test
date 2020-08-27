using System;

namespace ChibisTest.Features.Cart
{
    public class DeleteProductFromCartDto
    {
        public Guid CartId { get; set; }

        public int ProductId { get; set; }
    }
}

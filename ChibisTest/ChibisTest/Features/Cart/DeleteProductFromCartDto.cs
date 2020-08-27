using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChibisTest.Features.Cart
{
    public class DeleteProductFromCartDto
    {
        public Guid CartId { get; set; }

        public int ProductId { get; set; }
    }
}

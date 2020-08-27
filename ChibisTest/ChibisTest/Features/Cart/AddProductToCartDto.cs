using System;

namespace ChibisTest.Features.Cart
{
    public class AddProductToCartDto : DeleteProductFromCartDto
    {
        public bool ForBonusPoints { get; set; }
    }
}

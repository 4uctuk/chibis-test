using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChibisTest.Features.Cart
{
    public interface ICartExpiredService
    {
        Task RemoveExpiredCarts();
    }
}

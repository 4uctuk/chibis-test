using System;

namespace ChibisTest.Features.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
            
        }

        public EntityNotFoundException(string message) : base(message)
        {

        }
    }
}

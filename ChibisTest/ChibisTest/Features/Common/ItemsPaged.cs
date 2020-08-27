using System.Collections.Generic;
using ChibisTest.Features.DataAccess.Entities;

namespace ChibisTest.Features.Common
{
    public class ItemsPaged<T> where T : BaseEntity
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}

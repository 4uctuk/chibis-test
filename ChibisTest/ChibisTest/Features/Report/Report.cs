using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChibisTest.Features.Report
{
    public class Report
    {
        public int TotalCount { get; set; }

        public int CartsWithBonuses { get; set; }

        public int ExpiresIn10Days { get; set; }
        public int ExpiresIn20Days { get; set; }
        public int ExpiresIn30Days { get; set; }

        public decimal AverageCost { get; set; }
    }
}

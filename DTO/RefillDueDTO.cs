using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillService.DTO
{
    public class RefillDueDTO
    {
        public int SubscriptionId { get; set; }
        public int DueRefillTimes { get; set; }
    }
}

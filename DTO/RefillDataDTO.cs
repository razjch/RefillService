using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillService.DTO
{
    public class RefillDataDTO
    {
        public int DrugId { get; set; }
        public int ToalDrug { get; set; }
        public string RefillOccurrence { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public string Location { get; set; }
    }
}

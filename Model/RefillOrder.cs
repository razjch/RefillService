using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillService.Model
{
    public class RefillOrder
    {
        public int RefillOrderID { get; set; }
        public int SubscriptionID { get; set; }
        public DateTime RefillDate { get; set; }
        public int DueRefillTimes { get; set; }
        public int DrugId { get; set; }
        public double DuePrice { get; set; }

        public string Location { get; set; }
        //Completed or Pending
        public bool RefilStatus { get; set; }
    }
}

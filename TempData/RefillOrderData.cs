using RefillService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillService.TempData
{
    public class RefillOrderData
    {
        public List<RefillOrder> refillOrders;
        public RefillOrderData()
        {
            refillOrders = new List<RefillOrder>();
        }
    }
}

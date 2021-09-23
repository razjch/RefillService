using RefillService.DTO;
using RefillService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefillService.Repository
{
    public interface IRefillRepository
    {
        RefillOrder RequestAdhocRefill(SubscriptionDTO subscriptionDTO);
        bool CheckPendingPaymentStatus(int subscriptionId);
        RefillOrder ViewRefillStatus(int subscriptionId);
        RefillDueDTO GetRefillDuesAsOfDate(int subscriptionId, DateTime FromDate);
    }
}

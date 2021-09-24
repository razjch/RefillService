using Newtonsoft.Json;
using RefillService.DTO;
using RefillService.Model;
using RefillService.TempData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RefillService.Repository
{
    public class RefillRepository : IRefillRepository
    {
        private readonly RefillOrderData _refillOrderData;
        public RefillRepository(RefillOrderData refillOrderData)
        {
            _refillOrderData = refillOrderData;
        }
        public RefillOrder RequestAdhocRefill(SubscriptionDTO subscriptionDTO)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://52.154.250.104");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", subscriptionDTO.Token);

            //StringContent content = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync("/api/Subscription/getRefillData/" + subscriptionDTO.SubscriptionId).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string jsonString = response.Content.ReadAsStringAsync().Result;
                RefillDataDTO refillDTO = JsonConvert.DeserializeObject<RefillDataDTO>(jsonString);

                int id = _refillOrderData.refillOrders.Count;
                RefillOrder finalRefill = new RefillOrder();
                finalRefill.RefillOrderID = ++id;
                finalRefill.SubscriptionID = subscriptionDTO.SubscriptionId;
                finalRefill.DrugId = refillDTO.DrugId;
                finalRefill.RefillDate = DateTime.Now;
                finalRefill.Location = refillDTO.Location;

                var timeSpan = finalRefill.RefillDate.Subtract(refillDTO.SubscriptionDate);
                int remainingDaysToConsumeDrug = timeSpan.Days;

                if (refillDTO.RefillOccurrence.Equals("Weekly"))
                {
                    finalRefill.DueRefillTimes = remainingDaysToConsumeDrug / 7;
                }
                else
                {
                    finalRefill.DueRefillTimes = remainingDaysToConsumeDrug / 30;
                }
                if (remainingDaysToConsumeDrug == 0)
                {
                    finalRefill.DuePrice = 0;
                    finalRefill.RefilStatus = false;
                }
                else
                {
                    finalRefill.DuePrice = remainingDaysToConsumeDrug * 50;
                    finalRefill.RefilStatus = true;
                }

                _refillOrderData.refillOrders.Add(finalRefill);
                return finalRefill;
            }
            else
            {
                return null;
            }
        }

        public RefillDueDTO GetRefillDuesAsOfDate(int subscriptionId, DateTime FromDate)
        {
            var refillOrder = _refillOrderData.refillOrders.Find(r => r.SubscriptionID == subscriptionId);
            if (refillOrder == null)
                return null;

            int times = refillOrder.DueRefillTimes;
            RefillDueDTO res = new RefillDueDTO();
            if (FromDate > refillOrder.RefillDate)
                res.DueRefillTimes = times - 1;
            else
                res.DueRefillTimes = times + 1;
            res.SubscriptionId = subscriptionId;

            return res;
        }

        public RefillOrder ViewRefillStatus(int subscriptionId)
        {
            return _refillOrderData.refillOrders.Find(r => r.SubscriptionID == subscriptionId); 
        }
        public bool CheckPendingPaymentStatus(int subscriptionId)
        {
            var refillData = _refillOrderData.refillOrders.Find(r => r.SubscriptionID == subscriptionId);
            if (refillData == null)
                return true;
            if (refillData.DuePrice > 0)
                return false;
            return true;        

        }
    }
}

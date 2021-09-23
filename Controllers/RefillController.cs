using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefillService.DTO;
using RefillService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RefillService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RefillController : ControllerBase
    {
        private readonly IRefillRepository _refillRepository;
        public RefillController(IRefillRepository refillRepository)
        {
            _refillRepository = refillRepository;
        }

        [HttpPost("createRefill")]
        public IActionResult RequestAdhocRefill([FromBody]SubscriptionDTO subscriptionDTO)
        {
            var refilldata = _refillRepository.RequestAdhocRefill(subscriptionDTO);
            if (refilldata != null)
                return Ok(refilldata);
            return BadRequest("Unable to create Refill");
        }

        [HttpGet("checkPendingPaymentStatus/{subscriptionId}")]
        public ActionResult checkPendingPaymentStatus(int subscriptionId)
        {
            bool pendingStatus = _refillRepository.CheckPendingPaymentStatus(subscriptionId);
            if (pendingStatus == true)
                return Ok();
            else
                return BadRequest("Please Complete Payment for pending requests");
        }

        [HttpGet("GetRefillDues/{subscriptionId}/{Fromdate}")]
        public IActionResult GetRefillDuesAsOfDate(int subscriptionId, string date)
        {
            DateTime FromDate = Convert.ToDateTime(date);
            var res = _refillRepository.GetRefillDuesAsOfDate(subscriptionId, FromDate);
            if (res == null)
                return BadRequest("Subscription Id is Wrong...");
            return Ok(res);
        }

        [HttpGet("ViewRefillStatus/{subscriptionId}")]
        public IActionResult ViewRefillStatus(int subscriptionId)
        {
            var res = _refillRepository.ViewRefillStatus(subscriptionId);
            if (res == null)
                return BadRequest("Subscription Id is Wrong...");
            return Ok(res);
        }
    }
}

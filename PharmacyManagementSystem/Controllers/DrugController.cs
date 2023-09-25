using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Repository;
using System.Security.Claims;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class DrugController : ControllerBase
    {
        private IOrderInterface context;
        private IDrug context1;
        private IEmailInterface context2;
        private IUserInterface context3;
        private ILogger<DrugController> logger;
        public DrugController(IOrderInterface context, IDrug context1, IEmailInterface context2, IUserInterface context3, ILogger<DrugController> logger)
        {
            this.context = context;
            this.context1 = context1;
            this.context2 = context2;
            this.context3 = context3;
            this.logger = logger;
        }

        [HttpGet("Search Drug")]
        [AllowAnonymous]
        public IActionResult GetDrug(string DrugName)
        {
            var drugs = context1.GetDrug(DrugName);
            return Ok(drugs);
        }
        [HttpPost("Buy Drug")]
        public IActionResult Buy(int id, int quantity)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            int result = context.Buy(id, quantity, userId);
            if (result == 2)
            {
                logger.LogInformation("Drug order by user");
                return Ok("Ordered Successfully");
            }
            else if (result == 1)
            {
                logger.LogError("No such drug in database");
                return BadRequest("No such drug is available");
            }
            else
            {
                logger.LogError("quantity are not available");
                return BadRequest("quantity not avilable");
            }
        }

        [HttpGet("All Orders")]
        public IActionResult GetOrder()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            logger.LogInformation("Getting list of all the orders by user");
            return Ok(context.GetOrder(userId));
        }

        [HttpPost("Checkout")]
        public IActionResult Checkout(int id, int Payment)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var user = context3.GetUser(userId);
            int result = context.Checkout(id, Payment);
            if (result == 1)
            {
                logger.LogError("order not found for the id entered by user");
                return BadRequest("No such order found");
            }
            else if (result == 2)
            {
                logger.LogInformation("Transaction successfull for the order");
                string Subject = "Order Placed";
                string Body = "The order is successfully placed";
                context2.SendEmail(user.Email, Subject, Body);
                return Ok("Congratulation! The Payment Is Successfull.");
            }
            else if (result == 3)
            {
                logger.LogError("Transaction failed");
                return BadRequest("Payment failed\nNo amount was deducted");
            }
            else
            {
                logger.LogError("Quantity are not available");
                return BadRequest("Sorry drug are not available");
            }
        }
        [HttpPost("Cancel Order")]
        public IActionResult Cancel(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            bool result = context.Cancel(id);
            if (result)
            {
                string Subject = "Order Cancelled";
                string Body = "The Order is successfully cancelled";
                var user = context3.GetUser(userId);
                context2.SendEmail(user.Email, Subject, Body);
                logger.LogInformation("Order cancelled");
                return Ok("Order has been cancelled\nThe amount has been refunded");
            }
            else
            {
                logger.LogError("Unable to cancel the order");
                return BadRequest("No Such Order Found");
            }
        }

    }
}
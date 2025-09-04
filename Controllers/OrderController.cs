using Microsoft.AspNetCore.Mvc;
using GaumataWeb.Models;
using System.Threading.Tasks;

namespace GaumataWeb.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Save order to DB

            // TODO: Send notification (e.g. Twilio)

            return Ok(new { message = "Order received successfully" });
        }
    }
}

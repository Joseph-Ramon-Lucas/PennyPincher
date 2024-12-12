using Microsoft.AspNetCore.Mvc;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/management")]
    public class MoneyManagementController : Controller
    {
        private static List<int> Monies = new List<int>();

        // handle:
        // get money, set money
        // 

        [HttpGet]
        public List<int> getMonies()
        {
            return Monies;

        }

        [HttpPost]
        public ActionResult addMonies(int ammount)
        {
            Monies.Add(ammount);
            return Ok("Added "+ammount+" of money.");
        }

    }
}

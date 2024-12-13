using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/management")]

    
    public class MoneyManagementController : Controller
    {
        //defining projected cash flow
        static double income = 0;
        static double expense_food = 0;
        static double expense_living = 0;
        static double expense_personal = 0;



        [HttpGet("income")]
        public double getIncome()
        {
            return income;

        }

        [HttpGet("expenses")]
        public IEnumerable<double> getExpenses()
        {
            return [expense_food, expense_living, expense_personal];
        }

        [HttpPost("income")]
        public ActionResult setIncome(double ammount)
        {
            income = ammount;
            return Ok("Income set to "+ammount+" money.");
        }


        [HttpPost("expenses")]
        public ActionResult setExpenses(List<double> expenses)
        {
            if (expenses != null && expenses.Count > 0)
            {
                expense_food = expenses[0];
                expense_living = expenses[1];
                expense_personal = expenses[2];
                //returning with strings for testing
                return Ok("food: " + expense_food+"\nliving: "+expense_living+ "\npersonal: "+expense_personal);
            }
            else
            {
                return BadRequest(expenses + " must contain a list of 3 doubles");
            }
        }

    }
}

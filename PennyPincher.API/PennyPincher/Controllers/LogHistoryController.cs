using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;

namespace PennyPincher.Controllers
{
    [ApiController]
    [Route("/api/history")]
    public class LogHistoryController : Controller
    {
        private static List<ItemDto> allItems = new List<ItemDto>();

        [HttpGet]
        public List<ItemDto> GetItemHistory()
        {
            return allItems;
        }

        [HttpPost]
        public ActionResult addItem(ItemDto item)
        {
            allItems.Add(item);
            return Ok("Item was added.");
        }
    }    
}


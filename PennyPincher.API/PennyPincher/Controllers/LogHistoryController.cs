using Microsoft.AspNetCore.Mvc;
using PennyPincher.Models;

namespace PennyPincher.Controllers
{
    [Route("/api/history")]
    [ApiController]
    public class LogHistoryController : Controller
    {
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(ItemDto item)
        {
            ItemsDataStore.Current.Items.Add(item);
            return Ok(item);
        }
        
        [HttpGet]
        public ActionResult<List<ItemDto>> GetAllItems()
        {
            if (ItemsDataStore.Current.Items.Count == 0)
            {
                return NotFound();
            }

            return Ok(ItemsDataStore.Current.Items);
        }
        
        [HttpGet("{itemId}", Name = "GetItem")]
        public ActionResult<IEnumerable<ItemDto>> GetItem(int itemId)
        {
            var foundItem = ItemsDataStore.Current.Items.FirstOrDefault(i => i.Id == itemId);
            if (foundItem == null)
            {
                return NotFound();
            }
            
            return Ok(foundItem);
        }

        // TODO: Issue with updating item when following endpoint is hit. Need to investigate
        [HttpPut]
        public ActionResult<List<ItemDto>> UpdateItem(ItemDto newItem)
        {
            var itemToUpdate = ItemsDataStore.Current.Items.FirstOrDefault(i => i.Id == newItem.Id);
            if (itemToUpdate == null)
            {
                return NotFound("Item not found");
            }
            
            // Replace same item in data store with new item for update
            // TODO: Is there a more optimal way to update items in a list here?
            var place = ItemsDataStore.Current.Items.FindIndex(i => i.Id == newItem.Id);
            ItemsDataStore.Current.Items[place] = newItem;
            
            return Ok(ItemsDataStore.Current.Items);
        }

        // TODO: Issue with updating item when following endpoint is hit. Need to investigate
        [HttpDelete]
        public ActionResult<List<ItemDto>> DeleteItem(ItemDto itemToDelete)
        {
            ItemsDataStore.Current.Items.Remove(itemToDelete);
            if (ItemsDataStore.Current.Items.Contains(itemToDelete))
            {
                return NotFound("Error attempting to delete item");
            }   
            
            return Ok(ItemsDataStore.Current.Items);
        }
    }    
}


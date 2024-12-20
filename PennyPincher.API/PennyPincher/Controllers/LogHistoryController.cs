using PennyPincher.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpPut("{itemId}")]
        public ActionResult UpdateItem(int itemId, ItemForUpdateDto itemWithUpdate)
        {
            ItemDto itemFromStore = ItemsDataStore.Current.Items.FirstOrDefault(i => i.Id == itemId);
            if (itemFromStore == null)
            {
                return NotFound();
            }
            
            itemFromStore.Name = itemWithUpdate.Name; 
            itemFromStore.Price = itemWithUpdate.Price;
            
            return NoContent();
        }

        [HttpPatch("{itemId}")]
        public ActionResult PartiallyUpdateItem(int itemId, JsonPatchDocument<ItemForUpdateDto> patchDocument)
        {
            ItemDto itemFromStore = ItemsDataStore.Current.Items.FirstOrDefault(i => i.Id == itemId);
            if (itemFromStore == null)
            {
                return NotFound();
            }

            ItemForUpdateDto itemToPatch = new ItemForUpdateDto()
            {
                Name = itemFromStore.Name,
                Price = itemFromStore.Price
                // Category = itemFromStore.Category, TODO: Should we make Category field optional? User can have default input as null
            };
            
            patchDocument.ApplyTo(itemToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(itemToPatch))
            {
                return BadRequest(ModelState);  
            }
            
            itemFromStore.Name = itemToPatch.Name;
            itemFromStore.Price = itemToPatch.Price;
            
            return NoContent();   
        }
        
        [HttpDelete("{itemId}")]
        public ActionResult<List<ItemDto>> DeleteItem(ItemDto itemToDelete)
        {
            ItemDto item = ItemsDataStore.Current.Items.FirstOrDefault(i => i.Id == itemToDelete.Id);
            if (item == null)
            {
                return NotFound();
            }
            
            ItemsDataStore.Current.Items.Remove(item);
            return NoContent();
        }
    }    
}


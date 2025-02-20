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

            if (!ItemsDataStore.Current.Items.Contains(item))
            {
                return NotFound();
            }
            
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
        
        [HttpGet("{categoryType}/getcategoryitems")]
        public ActionResult<List<ItemDto>> GetItemByCategoryType(CategoryTypes categoryType)
        {
            if (categoryType == null)
            {
                return NotFound();
            }
            
            List<ItemDto> itemsByCategoryType = new List<ItemDto>();
            itemsByCategoryType = ItemsDataStore.Current.Items.Where(c => c.Category == categoryType).ToList();

            if (itemsByCategoryType.Count == 0)
            {
                return NotFound();  
            }
            
            return Ok(itemsByCategoryType);
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
            itemFromStore.Category = itemWithUpdate.Category;
            
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
        
        [HttpDelete]
        public ActionResult DeleteItem(ItemDto itemToDelete)
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


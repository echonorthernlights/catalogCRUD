using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using System;
using System.Linq;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]

    public class ItemsController : ControllerBase
    {

        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(),"Potion A","Potion A description", 7, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Potion B","Potion B description", 17, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Potion C","Potion C description", 27, System.DateTimeOffset.UtcNow)
        };

        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            return Ok("OK!!");
        }


        [HttpGet]
        public ActionResult<List<ItemDto>> GetList()
        {
            return Ok(items);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            //var item = items.Find(i => i.Id == id);
            var item = items.Where(i => i.Id == id).SingleOrDefault();
            if (item == null)
            {
                return NotFound("Item not found !!");
            }
            return Ok(item);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<ItemDto> DeleteItem(Guid id)
        {
            //var item = items.Find(i => i.Id == id);
            //var item = items.Where(i => i.Id == id).SingleOrDefault();

            var index = items.FindIndex(i => i.Id == id);
             if (index < 0)
             {
                 return NotFound("Item not found !!");
             }

            items.RemoveAt(index);
            return Ok(items);
        }


       [HttpPut]
         [Route("{id}")]
         public ActionResult<ItemDto> UpdateItem(Guid id, UpdateItemDto updatedItemDto)
         {
             //var item = items.Find(i => i.Id == id);
             var existingItem = items.Where(i => i.Id == id).SingleOrDefault();
             

             if (existingItem == null)
             {
                 return NotFound("Item not found !!");
             }

             var updatedItem = existingItem with {
                Name = updatedItemDto.Name,
                Description = updatedItemDto.Description,
                Price = updatedItemDto.Price
             };

            // var index = items.IndexOf(existingItem);
            var index = items.FindIndex(existingItem =>existingItem.Id == id);
            items[index] = updatedItem;
             return Ok(updatedItem);
         }
 
        [HttpPost]
        public ActionResult<ItemDto> PostItem(CreateItemDto newItem)
        {

            var NewItemDto = new ItemDto(Guid.NewGuid(), newItem.Name, newItem.Description, newItem.Price, DateTimeOffset.UtcNow);
            items.Add(NewItemDto);
            return Ok("Items created successfully !!");
        }

    }
}
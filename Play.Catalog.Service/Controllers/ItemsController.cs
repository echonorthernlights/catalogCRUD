using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]

    public class ItemsController : ControllerBase
    {

        /*private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(),"Potion A","Potion A description", 7, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Potion B","Potion B description", 17, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Potion C","Potion C description", 27, System.DateTimeOffset.UtcNow)
        };*/

        private readonly IItemsRepository itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            return Ok("OK!!");
        }


        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetListAsync()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            //var item = items.Find(i => i.Id == id);
            //var item = items.Where(i => i.Id == id).SingleOrDefault();
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound("Item not found !!");
            }
            return item.AsDto();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);
            if (item == null)
            {
                return NotFound("Item not found !!");
            }

            await itemsRepository.DeleteAsync(id);
            return NoContent();
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id, UpdateItemDto updatedItemDto)
        {
            //var item = items.Find(i => i.Id == id);
            var existingItem = await itemsRepository.GetAsync(id);


            if (existingItem == null)
            {
                return NotFound("Item not found !!");
            }

            existingItem.Name = updatedItemDto.Name;
            existingItem.Description = updatedItemDto.Description;
            existingItem.Price = updatedItemDto.Price;

            // var index = items.IndexOf(existingItem);
            //var index = items.FindIndex(existingItem => existingItem.Id == id);
            //items[index] = updatedItem;

            await itemsRepository.UpdateAsync(existingItem);
            return NoContent();
            //return Ok(updatedItem);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(CreateItemDto newItem)
        {

            var item = new Item { Name = newItem.Name, Description = newItem.Description, Price = newItem.Price, CreatedAt = DateTimeOffset.UtcNow };
            //var NewItemDto = new ItemDto(Guid.NewGuid(), newItem.Name, newItem.Description, newItem.Price, DateTimeOffset.UtcNow);
            await itemsRepository.CreateAsync(item);
            // ID format issue
            //return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item);
            return item.AsDto();
        }

    }
}
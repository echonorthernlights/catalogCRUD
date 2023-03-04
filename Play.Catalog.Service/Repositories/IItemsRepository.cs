using System;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;
using System.Collections.Generic;
namespace Play.Catalog.Service.Repositories
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item entity);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task UpdateAsync(Item entity);
    }

}
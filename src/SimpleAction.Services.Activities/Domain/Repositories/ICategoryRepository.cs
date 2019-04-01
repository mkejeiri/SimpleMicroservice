using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleAction.Services.Activities.Domain.Models;

namespace SimpleAction.Services.Activities.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IEnumerable<Category>> BrowseAsync();
        Task AddAsync(Category category);
    }
}
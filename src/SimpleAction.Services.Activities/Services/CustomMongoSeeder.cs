using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SimpleAction.Common.Mongo;
using SimpleAction.Services.Activities.Domain.Models;
using SimpleAction.Services.Activities.Repositories;

namespace SimpleAction.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _categoryRepository;

        public CustomMongoSeeder(IMongoDatabase database, 
        ICategoryRepository categoryRepository) : base(database)
        {
            this._categoryRepository = categoryRepository;
        }
        protected override async Task CustomSeedAsync(){
            var categories = new List<string>{
                "Sport",
                "Travel",
                "Learning"
            };

            await Task.WhenAll(categories.Select (x => _categoryRepository.AddAsync(new Category(x))));    
        }
    }
}
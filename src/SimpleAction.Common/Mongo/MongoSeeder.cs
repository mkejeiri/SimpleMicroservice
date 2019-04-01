using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SimpleAction.Common.Mongo
{
    public class MongoSeeder : IDatabaseSeeder
    {
        protected readonly IMongoDatabase database;

        public MongoSeeder(IMongoDatabase database)
        {
            this.database = database;
        }

        public async Task SeedAsync()
        {
            var collectionsRef = await database.ListCollectionsAsync();
            var  collections = await collectionsRef.ToListAsync();
            if (collections.Any())
            {
                return;
            }
            await CustomSeedAsync();            
            
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
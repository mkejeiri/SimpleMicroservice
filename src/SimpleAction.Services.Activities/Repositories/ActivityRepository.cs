using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SimpleAction.Services.Activities.Domain.Models;

namespace SimpleAction.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository

    {
        private readonly IMongoDatabase _database ;

        public ActivityRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(Activity activity) => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetAsync(Guid id) => await Collection.AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(id));      

         private IMongoCollection<Activity> Collection => _database.GetCollection<Activity>("Activities");
    }
}
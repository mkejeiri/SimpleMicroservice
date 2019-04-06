using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SimpleAction.Api.Models;

namespace SimpleAction.Api.Repositories {
    public class ActivityRepository : IActivityRepository {

        private readonly IMongoDatabase _database;

        public ActivityRepository (IMongoDatabase database) {
            _database = database;
        }
        public async Task AddAsync (Activity activity) => await Collection.InsertOneAsync (activity);

        public async Task<IEnumerable<Activity>> BrowseAsync (Guid userID) => await Collection
            .AsQueryable ()
            .Where (x => x.UserId.Equals (userID)).ToListAsync ();

        public async Task<Activity> GetAsync (Guid id) => await Collection.AsQueryable ().FirstOrDefaultAsync (x => x.Id.Equals (id));
        private IMongoCollection<Activity> Collection => _database.GetCollection<Activity> ("Activities");
    }
}
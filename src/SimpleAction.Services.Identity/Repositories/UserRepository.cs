using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SimpleAction.Services.Identity.Domain.Models;
using SimpleAction.Services.Identity.Domain.Repositories;

namespace SimpleAction.Services.Identity.Repositories {
    public class UserRepository : IUserRepository {
        private readonly IMongoDatabase _database;
        public UserRepository (IMongoDatabase database) {
            _database = database;
        }

        public async Task<User> GetAsync (string email) =>  await Collection.AsQueryable ().FirstOrDefaultAsync (x => email.Equals (x.Email,StringComparison.OrdinalIgnoreCase));
        public async Task AddAsync (User user) => await Collection.InsertOneAsync (user);

        public async Task<User> GetAsync (Guid id) => await Collection.AsQueryable ().FirstOrDefaultAsync (x => x.Id.Equals (id));

        private IMongoCollection<User> Collection => _database.GetCollection<User> ("Users");

    }
}
using System;
using System.Threading.Tasks;
using SimpleAction.Services.Identity.Domain.Models;

namespace SimpleAction.Services.Identity.Domain.Repositories {
    public interface IUserRepository {
        Task<User> GetAsync (Guid Id);
        Task<User> GetAsync (string Email);
        Task AddAsync (User user);
    }
}
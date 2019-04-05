using System.Threading.Tasks;
using SimpleAction.Common.Exceptions;
using SimpleAction.Services.Identity.Domain.Models;
using SimpleAction.Services.Identity.Domain.Repositories;
using SimpleAction.Services.Identity.Domain.Services;

namespace SimpleAction.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository,  IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task LoginAsync(string email, string password)
        {
           var user = await _userRepository.GetAsync(email);
           if (user == null)
           {
               throw new ActionException("invalid_credentials", $"Invalid email/password!");
           }


            if (user.ValidatePassword(password, _encrypter))
           {
               throw new ActionException("invalid_credentials", $"Invalid email/password!");
           }
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
           var user = await _userRepository.GetAsync(email);
           if (user == null)
           {
               throw new ActionException("email_in_use", $"Email: '{email}' is already in use");
           }
           user = new User(email, name);
           user.setPassword(password, _encrypter);
           await _userRepository.AddAsync(user);
        }
    }
}
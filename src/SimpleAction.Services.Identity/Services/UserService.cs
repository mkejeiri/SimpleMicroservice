using System.Threading.Tasks;
using SimpleAction.Common.Auth;
using SimpleAction.Common.Exceptions;
using SimpleAction.Services.Identity.Domain.Models;
using SimpleAction.Services.Identity.Domain.Repositories;
using SimpleAction.Services.Identity.Domain.Services;

namespace SimpleAction.Services.Identity.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService (IUserRepository userRepository,
            IEncrypter encrypter,
            IJwtHandler jwtwtHandler) {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtwtHandler;
        }

        public async Task<MyJsonWebToken>  LoginAsync (string email, string password) {
            var user = await _userRepository.GetAsync (email);
            if (user == null) {
                throw new ActionException ("invalid_credentials", $"Invalid email/password!");
            }

            if (user.ValidatePassword (password, _encrypter)) {
                throw new ActionException ("invalid_credentials", $"Invalid email/password!");
            }
            return _jwtHandler.Create (user.Id);
        }

        public async Task RegisterAsync (string email, string password, string name) {
            var user = await _userRepository.GetAsync (email);
            if (user == null) {
                throw new ActionException ("email_in_use", $"Email: '{email}' is already in use");
            }
            user = new User (email, name);
            user.setPassword (password, _encrypter);
            await _userRepository.AddAsync (user);
            // return _jwtHandler.Create (user.Id);
        }
    }
}
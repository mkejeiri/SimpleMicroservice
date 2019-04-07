using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using SimpleAction.Common.Auth;
using SimpleAction.Services.Identity.Domain.Models;
using SimpleAction.Services.Identity.Domain.Repositories;
using SimpleAction.Services.Identity.Domain.Services;
using SimpleAction.Services.Identity.Services;
using Xunit;

namespace SimpleAction.Services.Identity.Tests.Unit.Services {
    public class UserServiceTest {
        [Fact]
        public async Task user_service_login_should_return_jwt_token () {
            var userId = Guid.NewGuid();
            var email = "test@test.com";
            var password = "secret";
            var name = "test";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            var utcNow = DateTime.UtcNow;

            var jwtHandlerMock = new Mock<IJwtHandler> ();
            jwtHandlerMock.Setup (x => x.Create (It.IsAny<Guid> ())).Returns (new MyJsonWebToken {
                Token = token
            });

            var encrypterMock = new Mock<IEncrypter> ();
            encrypterMock.Setup (x => x.GetSalt ()).Returns (salt);
            encrypterMock.Setup (x => x.GetHash (password, salt)).Returns (hash);

            var user = new User (userId,email,name, password, salt,utcNow);
            user.setPassword (password, encrypterMock.Object);

            var userRespositoryMock = new Mock<IUserRepository> ();
            userRespositoryMock.Setup (x => x.GetAsync (email)).ReturnsAsync (user);

            var userService = new UserService (userRespositoryMock.Object, encrypterMock.Object, jwtHandlerMock.Object);

            var jwt = await userService.LoginAsync (email, password);
            userRespositoryMock.Verify (x => x.GetAsync (email), Times.Once);
            jwtHandlerMock.Verify (x => x.Create(It.IsAny<Guid>()), Times.Once);
            jwt.Token.Should ().BeEquivalentTo (token);
        }
    }
}
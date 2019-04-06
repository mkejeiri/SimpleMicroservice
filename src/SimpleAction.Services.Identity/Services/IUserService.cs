using System.Threading.Tasks;
using SimpleAction.Common.Auth;

namespace SimpleAction.Services.Identity.Services
{
    public interface IUserService
    {
         Task RegisterAsync(string email, string password, string name);
         //this will return a token
         Task<MyJsonWebToken> LoginAsync(string email, string password);
         
    }
}
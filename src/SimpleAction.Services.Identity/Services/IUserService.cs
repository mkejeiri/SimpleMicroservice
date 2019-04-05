using System.Threading.Tasks;

namespace SimpleAction.Services.Identity.Services
{
    public interface IUserService
    {
         Task RegisterAsync(string email, string password, string name);
         //this will return a token
         Task LoginAsync(string email, string password);
         
    }
}
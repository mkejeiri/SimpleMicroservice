//used for command handler pattern
using System.Threading.Tasks;

namespace SimpleAction.Common.Commands
{
    public interface ICommandHandler<in T> where T: ICommand
    {
         Task HandleAsync(T command);
    }
}
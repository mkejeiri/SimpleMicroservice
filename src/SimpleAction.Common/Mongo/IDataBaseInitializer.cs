using System.Threading.Tasks;

namespace SimpleAction.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}
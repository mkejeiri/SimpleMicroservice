using System.Threading.Tasks;

namespace SimpleAction.Common.Mongo
{
    public interface IDatabaseSeeder
    {
         Task SeedAsync();
    }
}
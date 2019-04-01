using System;
using System.Threading.Tasks;
using SimpleAction.Services.Activities.Domain.Models;

namespace SimpleAction.Services.Activities.Repositories
{
    public interface IActivityRepository
    {
         Task<Activity> GetAsync(Guid Id);
         Task AddAsync(Activity activity);
    }
}
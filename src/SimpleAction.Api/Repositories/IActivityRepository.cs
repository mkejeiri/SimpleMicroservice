using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleAction.Api.Models;

namespace SimpleAction.Api.Repositories
{
    public interface IActivityRepository
    {

        Task<Activity> GetAsync(Guid Id);
        Task AddAsync(Activity model);

        Task<IEnumerable<Activity>> BrowseAsync(Guid userID);
    }
}
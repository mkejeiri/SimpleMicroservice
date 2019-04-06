using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleAction.Api.Models;

namespace SimpleAction.Api.Services {
    public interface IActivityService {
        Task AddAsync (Guid id, Guid userId, string category, string name, string description, DateTime createdAt);
        Task<IEnumerable<ActivityDto>> GetActivitiesAsync (Guid userId);
        Task<ActivityDto> GetActivityAsync (Guid Id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleAction.Api.Models;
using SimpleAction.Api.Repositories;
using SimpleAction.Common.Exceptions;

namespace SimpleAction.Api.Services {
    public class ActivityService : IActivityService

    {
        public ActivityService (IActivityRepository activityRepository) {
            _activityRepository = activityRepository;

        }
        private readonly IActivityRepository _activityRepository;

        public async Task AddAsync (Guid id, Guid userId, string category, string name, string description, DateTime createdAt) {
            var activity = new Activity (id, userId, name, category, description, createdAt);
            await _activityRepository.AddAsync (activity);
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesAsync (Guid userId) {
            return ((IEnumerable<Activity>) await _activityRepository.BrowseAsync ((Guid) userId))
                .Select (x => new ActivityDto {
                    Id = x.Id,
                        UserId = x.UserId,
                        Name = x.Name,
                        Category = x.Category,
                        CreatedAt = x.CreatedAt
                });
        }

        public async Task<ActivityDto> GetActivityAsync (Guid Id) {
            var activity = await _activityRepository.GetAsync (Id);
            return new ActivityDto {
                Id = activity.Id,
                    UserId = activity.UserId,
                    Name = activity.Name,
                    Category = activity.Category,
                    CreatedAt = activity.CreatedAt
            };
        }
    }
}
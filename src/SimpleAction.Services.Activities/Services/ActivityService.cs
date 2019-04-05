using System;
using System.Threading.Tasks;
using SimpleAction.Common.Exceptions;
using SimpleAction.Services.Activities.Domain.Models;
using SimpleAction.Services.Activities.Repositories;

namespace SimpleAction.Services.Activities.Services {
    public class ActivityService : IActivityService

    {
        public ActivityService (IActivityRepository activityRepository, ICategoryRepository categoryRepository) {
            this._activityRepository = activityRepository;
            _categoryRepository=categoryRepository;

        }
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;
        // public ActivityService () { }

        public async Task AddAsync (Guid id, Guid userId, string category, string name, string description, DateTime createdAt) {
           var activityCategory = await _categoryRepository.GetAsync(name);
           if (activityCategory == null)
           {
               throw new ActionException("category_not_found", $"Category: '{category}' was not found");
           }  
           var activity = new Activity(id, activityCategory, userId, name, description, createdAt);
           await _activityRepository.AddAsync(activity);

             
        }
    }
}
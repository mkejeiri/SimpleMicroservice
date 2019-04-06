using System;
using SimpleAction.Common.Exceptions;

namespace SimpleAction.Api.Models
{
    //Flat object : ready to be sent to the call using Api-storage db and not the activity services db
    // when an activity is created though the service activity, this will be pushed to SimpleActionApi and stored on its db

   public class Activity {
        public Activity () { }

        public Activity (Guid id, Guid userId, string category, string name, string description, DateTime createdAt) {
            if (string.IsNullOrWhiteSpace(name))
            {
                 throw new ActionException("empty_activity_name", "Acticity name cannot be empty");
            }
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.UserId = userId;
            this.Description = description;
            this.CreatedAt = createdAt;

        }
        public Guid Id { get;  set; }
        public string Name { get;  set; }
        public string Category { get;  set; }
        public Guid UserId { get;  set; }
        public string Description { get;  set; }
        public DateTime CreatedAt { get;  set; }

    }
}
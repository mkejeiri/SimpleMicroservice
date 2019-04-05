using System;
using SimpleAction.Common.Exceptions;

namespace SimpleAction.Services.Activities.Domain.Models {
    public class Activity {
        public Activity () { }

        public Activity (Guid id, Category category, Guid userId, string name, string description, DateTime createAt) {
            if (string.IsNullOrWhiteSpace(name))
            {
                 throw new ActionException("empty_activity_name", "Acticity name cannot be empty");
            }
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.UserId = userId;
            this.Description = description;
            this.CreateAt = createAt;

        }
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreateAt { get; protected set; }

    }
}
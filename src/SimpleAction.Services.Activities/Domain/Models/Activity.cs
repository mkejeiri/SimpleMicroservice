using System;

namespace SimpleAction.Services.Activities.Domain.Models {
    public class Activity {
        public Activity () { }

        public Activity (Guid id, string name, string category, Guid userId,  string description, DateTime createAt) {
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.UserId = userId;
            this.Description = description;
            this.CreateAt = createAt;

        }
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreateAt { get; protected set; }

    }
}
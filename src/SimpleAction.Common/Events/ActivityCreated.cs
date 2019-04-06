using System;

namespace SimpleAction.Common.Events {
    public class ActivityCreated : IAuthenticatedEvent {
        protected ActivityCreated()
        {
        }

        public ActivityCreated (Guid Id,Guid userId, string category, string name, string description, DateTime createdAt) {
            this.Id = Id;
            this.UserId = userId;
            this.Category = category;
            this.Name = name;
            this.Description = description;
            this.CreatedAt = createdAt;

        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
        public DateTime CreatedAt { get; }

    }
}
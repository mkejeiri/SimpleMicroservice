using System;

namespace SimpleAction.Services.Activities.Domain.Models {
    public class Category {
        public Category (string name) { 
            Id= Guid.NewGuid();
            Name= name.ToLowerInvariant();
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

    }
}
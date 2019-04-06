using System;

namespace SimpleAction.Api.Models
{
    public class ActivityDto
    {
        public Guid Id { get;  set; }
         public string Name { get;  set; }
        public string Category { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public Guid UserId { get; internal set; }
    }
}
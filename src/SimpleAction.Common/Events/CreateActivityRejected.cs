using System;

namespace SimpleAction.Common.Events {
    public class CreateActivityRejected : IRejectedEvent {
        protected CreateActivityRejected () {}

        public CreateActivityRejected (Guid id,string code, string reason) {
            this.Reason = reason;
            this.Code = code;
            this.Id = id;
        }
        public string Reason { get; set; }
        public string Code { get; set; }
        public Guid Id { get; set; }
    }
}
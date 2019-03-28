namespace SimpleAction.Common.Events {
    public class CreateActivityRejected : IRejectedEvent {
        protected CreateActivityRejected () { }

        public CreateActivityRejected (string reason, string code, string name) {
            this.Reason = reason;
            this.code = code;
            this.Name = name;

        }
        public string Reason { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
    }
}
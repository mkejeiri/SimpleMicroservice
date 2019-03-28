namespace SimpleAction.Common.Events {
    public class CreateUserRejected : IRejectedEvent {
        protected CreateUserRejected () { }

        public CreateUserRejected (string email, string reason, string code) {
            this.Email = email;
            this.Reason = reason;
            this.code = code;

        }
        public string Email { get; }
        public string Reason { get; }
        public string code { get; }
    }
}
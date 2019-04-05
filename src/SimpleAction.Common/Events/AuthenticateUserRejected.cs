namespace SimpleAction.Common.Events {
    public class AuthenticateUserRejected : IRejectedEvent {
        protected AuthenticateUserRejected () { }

        public AuthenticateUserRejected (string email, string reason, string code) {
            this.Email = email;
            this.Reason = reason;
            this.Code = code;

        }
        public string Email { get; }
        public string Reason { get; }
        public string Code { get; }
    }
}
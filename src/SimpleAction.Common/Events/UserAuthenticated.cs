namespace SimpleAction.Common.Events {
    public class UserAuthenticated : IEvent {
        protected UserAuthenticated()
        {
        }

        public UserAuthenticated (string email) {
            this.Email = email;

        }
        public string Email { get; }

    }
}
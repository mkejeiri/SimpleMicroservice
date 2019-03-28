namespace SimpleAction.Common.Events {
    public class UserCreated : IEvent {
        //to avoid any issue with serializer
        protected UserCreated () { }

        public UserCreated (string email, string name) {
            this.Email = email;
            this.Name = name;

        }
        public string Email { get; }       
        public string Name { get; }

    }
}
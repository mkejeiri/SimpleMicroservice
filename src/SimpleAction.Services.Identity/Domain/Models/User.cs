using System;
using System.Text.RegularExpressions;
using SimpleAction.Common.Exceptions;
using SimpleAction.Services.Identity.Domain.Services;

namespace SimpleAction.Services.Identity.Domain.Models {
    public class User {
        protected User () { }

        public User (string name, string email) {

            if (string.IsNullOrWhiteSpace (email)) {
                throw new ActionException ("empty_User_email", "User email cannot be empty");
            }

            if (!Regex.IsMatch (email,
                    @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")) {
                throw new ActionException ("Invalid_User_email", "Invalid User email");
            }

            if (string.IsNullOrWhiteSpace (name)) {
                throw new ActionException ("empty_User_name", "User name cannot be empty");
            }

            this.Email = email.ToLowerInvariant ();
            this.Name = name;
            this.CreatedAt = DateTime.UtcNow;

        }

        public User (Guid id, string email, string name, string password, string salt, DateTime createdAt) {
            this.Id = id;
            this.Email = email.ToLowerInvariant ();
            this.Name = name;
            this.Password = password;
            this.Salt = salt;
            this.CreatedAt = createdAt;

        }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }

        public void setPassword(string password, IEncrypter encrypter){
             if (string.IsNullOrWhiteSpace (password)) {
                throw new ActionException ("empty_User_password", "User password cannot be empty");
            }

            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }
        public bool ValidatePassword(string password, IEncrypter encrypter) => password.Equals(encrypter.GetHash(password, Salt));
    }
}
using System;
using System.Security.Cryptography;

namespace SimpleAction.Services.Identity.Domain.Services {
    public class Encrypter : IEncrypter {
        private readonly int SaltSSize = 40;
        private readonly int DeriveBytesIterationsCount = 10000;

        public string GetSalt () {
            var random = new Random ();
            var saltBytes = new byte[SaltSSize];
            var rng = RandomNumberGenerator.Create ();

            //fill with cryptographically strong random bytes.
            rng.GetBytes (saltBytes);

            //Converts an array of 8-bit unsigned integers to its equivalent string representation : see Base64 table
            return Convert.ToBase64String (saltBytes);
        }

        public string GetHash (string value, string salt) {
            var pbkdf2 = new Rfc2898DeriveBytes (value, GetBytes (salt), DeriveBytesIterationsCount);
            return Convert.ToBase64String (pbkdf2.GetBytes (SaltSSize));

        }

        private static byte[] GetBytes (string value) {
            var bytes = new byte[value.Length * sizeof (char)];
            // Copies a specified number of bytes from a source array starting at a particular
            // offset to a destination array starting at a particular offset.
            Buffer.BlockCopy (value.ToCharArray (), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
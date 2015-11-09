using System;
using System.Collections.Generic;

namespace VeInteractive.OTPEngine.DataStorage
{
    public class PasswordStore : IPasswordStore
    {
        private Dictionary<string, OneTimePassword> store = new Dictionary<string, OneTimePassword>();

        public OneTimePassword Read(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId");

            return (this.store.ContainsKey(userId)) ? this.store[userId] : null;
        }

        public void Write(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");

            var newPassword = new OneTimePassword(userId, password);

            if (this.store.ContainsKey(userId)) this.store.Remove(userId);
            
            this.store.Add(userId, newPassword);
        }
    }
}

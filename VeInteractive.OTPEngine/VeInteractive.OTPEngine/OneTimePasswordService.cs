using System;
using VeInteractive.OTPEngine.Configuration;
using VeInteractive.OTPEngine.DataStorage;

namespace VeInteractive.OTPEngine
{
    public class OneTimePasswordService : IOneTimePasswordService
    {
        private IPasswordStore passwordStore;
        private IConfiguration configuration;

        public OneTimePasswordService(IPasswordStore passwordStore, IConfiguration configuration)
        {
            this.passwordStore = passwordStore;
            this.configuration = configuration;
        }

        public string Generate(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId");

            var guid = Guid.NewGuid();
            string password = guid.ToString().Split('-')[0];

            this.passwordStore.Write(userId, password);

            return password;
        }

        public bool Validate(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException("userId");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");

            var passwordRecord = this.passwordStore.Read(userId);

            if (passwordRecord == null || passwordRecord.Password != password) return false;

            int expireyInSeconds;
            if(!int.TryParse(this.configuration.Read("passwordExpiryInSeconds"), out expireyInSeconds)) expireyInSeconds = 30; // apply default if parse fails.
            
            if (DateTime.UtcNow.Subtract(passwordRecord.Timestamp) > new TimeSpan(0, 0, 30)) return false;

            return true;
        }
    }
}

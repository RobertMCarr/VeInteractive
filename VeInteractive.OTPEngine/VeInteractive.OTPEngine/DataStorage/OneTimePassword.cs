/********************************************
 ** Author: Robert Carr                    **
 ** Code Test Submission for VeInteractive **
 ********************************************/

using System;

namespace VeInteractive.OTPEngine.DataStorage
{
    public sealed class OneTimePassword
    {
        public string Password { get; internal set; }
        public string UserId { get; internal set; }
        public DateTime Timestamp { get; internal set; }

        public OneTimePassword(string userId, string password)
        {
            this.Password = password;
            this.UserId = userId;
            this.Timestamp = DateTime.UtcNow;
        }
    }
}

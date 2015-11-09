/********************************************
 ** Author: Robert Carr                    **
 ** Code Test Submission for VeInteractive **
 ********************************************/

namespace VeInteractive.OTPEngine.DataStorage
{
    public interface IPasswordStore
    {
        OneTimePassword Read(string userId);
        void Write(string userId, string password);
    }
}
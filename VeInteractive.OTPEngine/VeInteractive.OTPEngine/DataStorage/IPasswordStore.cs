namespace VeInteractive.OTPEngine.DataStorage
{
    public interface IPasswordStore
    {
        OneTimePassword Read(string userId);
        void Write(string userId, string password);
    }
}
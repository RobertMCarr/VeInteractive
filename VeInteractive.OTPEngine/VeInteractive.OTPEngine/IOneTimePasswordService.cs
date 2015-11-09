namespace VeInteractive.OTPEngine
{
    public interface IOneTimePasswordService
    {
        string Generate(string userId);
        bool Validate(string userId, string password);
    }
}
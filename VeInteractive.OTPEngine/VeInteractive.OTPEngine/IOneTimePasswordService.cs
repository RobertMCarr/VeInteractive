/********************************************
 ** Author: Robert Carr                    **
 ** Code Test Submission for VeInteractive **
 ********************************************/
 
 namespace VeInteractive.OTPEngine
{
    public interface IOneTimePasswordService
    {
        string Generate(string userId);
        bool Validate(string userId, string password);
    }
}
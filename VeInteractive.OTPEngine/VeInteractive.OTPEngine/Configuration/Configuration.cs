/********************************************
 ** Author: Robert Carr                    **
 ** Code Test Submission for VeInteractive **
 ********************************************/

using System.Configuration;

namespace VeInteractive.OTPEngine.Configuration
{
    public class Configuration : IConfiguration
    {
        public string Read(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}

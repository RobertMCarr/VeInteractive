/********************************************
 ** Author: Robert Carr                    **
 ** Code Test Submission for VeInteractive **
 ********************************************/
 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading;
using VeInteractive.OTPEngine.Configuration;
using VeInteractive.OTPEngine.DataStorage;

namespace VeInteractive.OTPEngine.Tests
{
    [TestClass]
    public class OneTimePasswordServiceTests
    {
        private Mock<IPasswordStore> passwordStoreMoq = new Mock<IPasswordStore>();
        private Mock<IConfiguration> configurationMoq = new Mock<IConfiguration>();
        private string userId = "CheckYeager";
        private int expiryInSeconds = 5;

        [TestInitialize]
        public void Setup()
        {
            passwordStoreMoq.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>()));
            configurationMoq.Setup(x => x.Read(It.IsAny<string>())).Returns(expiryInSeconds.ToString());
        }

        [TestMethod]
        public void GenerateUniquePasswordTest()
        {
            var oneTimePasswordService = new OneTimePasswordService(this.passwordStoreMoq.Object, this.configurationMoq.Object);

            var passwords = new List<string>();
            string password = string.Empty;
            for (int i = 0; i < 100; i++)
            {
                password = oneTimePasswordService.Generate(userId);
                if (passwords.Contains(password)) Assert.Fail("Password was not unique.");

                passwords.Add(password);
            }
        }

        [TestMethod]
        public void PasswordShouldFailValidationAfterExpiryTime()
        {
            var oneTimePasswordService = new OneTimePasswordService(this.passwordStoreMoq.Object, this.configurationMoq.Object);

            var password = oneTimePasswordService.Generate(userId);
            Thread.Sleep((expiryInSeconds + 1) * 1000);

            if (oneTimePasswordService.Validate(this.userId, password)) Assert.Fail("Password should have expired.");
        }

        [TestMethod]
        public void PasswordShouldValidateBeforeExpiry()
        {
            var otp = new OneTimePassword(this.userId, "testPassword");
            passwordStoreMoq.Setup(x => x.Read(this.userId)).Returns(otp);

            var oneTimePasswordService = new OneTimePasswordService(this.passwordStoreMoq.Object, this.configurationMoq.Object);
            var password = oneTimePasswordService.Generate(userId);
            if (!oneTimePasswordService.Validate(this.userId, otp.Password)) Assert.Fail("Password should have validated.");
        }
    }
}

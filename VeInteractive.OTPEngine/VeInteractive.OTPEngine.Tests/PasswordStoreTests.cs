using Microsoft.VisualStudio.TestTools.UnitTesting;
using VeInteractive.OTPEngine.DataStorage;

namespace VeInteractive.OTPEngine.Tests
{
    [TestClass]
    public class PasswordStoreTests
    {
        private PasswordStore passwordStore;

        [TestInitialize]
        public void Setup()
        {
            this.passwordStore = new PasswordStore();
        }

        [TestMethod]
        public void ShouldReturnNullIfUserIdIsNotInStore()
        {
            Assert.IsNull(this.passwordStore.Read("ChuckYeager"));
        }

        [TestMethod]
        public void ShouldNotReturnNullIfUserIdIsInStore()
        {
            this.passwordStore.Write("ChuckYeager", "testPassword");
            Assert.IsNotNull(passwordStore.Read("ChuckYeager"));
        }
    }
}

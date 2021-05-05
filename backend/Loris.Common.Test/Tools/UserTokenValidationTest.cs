using System;
using Loris.Common.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loris.Common.Test.Tools
{
    [TestClass]
    public class UserTokenValidationTest
    {
        private static readonly string UserKey = new Guid().ToString();
        //private const string LoginId = "012345678901234567890123456789";
        private const string LoginId = "XXXXXXXXXXXXXXXXXXXXXXXXXadmin";

        [TestMethod]
        public void TestUserTokenValidationWithKey()
        {
            try
            {
                var token = UserTokenValidation.GenerateToken(LoginId, UserKey);
                var staValidation = UserTokenValidation.ValidateToken(LoginId, UserKey, token);

                Assert.IsTrue(staValidation.Count == 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestUserTokenValidationWithoutKey()
        {
            try
            {
                var token = UserTokenValidation.GenerateToken(LoginId);
                var staValidation = UserTokenValidation.ValidateToken(token, LoginId);

                Assert.IsTrue(staValidation.Count == 0);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

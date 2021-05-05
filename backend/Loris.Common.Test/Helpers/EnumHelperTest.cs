using Loris.Common.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loris.Resources;
using System;

namespace Loris.Common.Test.Helpers
{
    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void TestGetDescriptionFromEnum()
        {
            try
            {
                var dic = EnumHelper.GetDictionaryFromEnum(TimeSpanEnum.Undefined, BASERES.ResourceManager);
                if (string.IsNullOrEmpty(dic))
                    Assert.Fail("Não foi encontrado o dicionário!");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestGetEnumModel()
        {
            try
            {
                var model = EnumHelper.GetEnumModel<TimeSpanEnum>(BASERES.ResourceManager);
                if (model.Count == 0)
                    Assert.Fail("Não gerou domínio do enumerador!");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}

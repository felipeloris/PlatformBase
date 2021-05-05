using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loris.Common.Tools;
using Loris.Common.Domain.Entities;
using Loris.Common.Helpers;

namespace Loris.Common.Test.Helpers
{
    [TestClass]
    public class DatabaseHelperTest
    {
        public static Database Database { get; } = new Database
        {
            Server = "127.0.0.1",
            Port = 5432,
            DbName = "LorisCommon",
            UserId = "migrations",
            Password = "4wk4nS4NNzErF@_"
        };

        [TestMethod]
        public void TestCryptAndDecryptDatabase()
        {
            var configPath = GlobalFunction.ReadConfigValue("Loris:ConfigPath");
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            var configDbFile = GlobalFunction.ReadConfigValue("Loris:ConfigDbFile");
            var file = Path.Combine(configPath, configDbFile);

            DatabaseHelper.SaveDatabase(Database, file);

            var db2 = DatabaseHelper.RecoverDatabase(file);

            Assert.IsTrue(Database.Server.Equals(db2.Server));
            Assert.IsTrue(Database.Port.Equals(db2.Port));
            Assert.IsTrue(Database.DbName.Equals(db2.DbName));
            Assert.IsTrue(Database.UserId.Equals(db2.UserId));
            Assert.IsTrue(Database.Password.Equals(db2.Password));
        }
    }
}

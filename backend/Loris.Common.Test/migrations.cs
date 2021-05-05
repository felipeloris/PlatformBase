using Loris.Infra.Context;
using Loris.Common.Domain.Entities;
using Loris.Common.EF.Helpers;
using Loris.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Loris.Common.Test
{
    [TestClass]
    public class migrations
    {
        private static Database Database { get; } = DatabaseHelper.RecoverDatabase();

        [TestMethod]
        public void AuthDbContext()
        {
            var context = EfCoreHelper.GetContext<LorisContext>(Database.ConnString);
            context.Database.Migrate();
        }
    }
}

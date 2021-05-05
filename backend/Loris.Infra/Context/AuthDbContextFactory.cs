using Loris.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Loris.Infra.Context
{
    public class AuthDbContextFactory : IDesignTimeDbContextFactory<LorisContext>
    {
        public LorisContext CreateDbContext(string[] args)
        {
            // todo - deixar dinâmico o caminho do arquivo 'dbconfig'
            var optionsBuilder = new DbContextOptionsBuilder<LorisContext>();
            optionsBuilder.UseNpgsql(
                DatabaseHelper.RecoverDatabase(@"D:\Projetos\LorisPlatformBase\loris.dbconfig").ConnString, 
                opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new LorisContext(optionsBuilder.Options);
        }
    }
}

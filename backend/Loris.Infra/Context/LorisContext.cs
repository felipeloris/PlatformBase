using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Loris.Infra.Context
{
    public partial class LorisContext : DbContext
    {
        public LorisContext() : base()
        {
        }

        public LorisContext(DbContextOptions<LorisContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
                //.HasAnnotation("ProductVersion", "3.1.9")
                //.HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.ApplyConfiguration(new AuthResourceConfiguration());
            modelBuilder.ApplyConfiguration(new AuthRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AuthRoleResourceConfiguration());
            modelBuilder.ApplyConfiguration(new AuthUserConfiguration());
            modelBuilder.ApplyConfiguration(new AuthUserRoleConfiguration());

            modelBuilder.ApplyConfiguration(new CourierMessageConfiguration());
            modelBuilder.ApplyConfiguration(new CourierToConfiguration());
            modelBuilder.ApplyConfiguration(new CourierTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new CourierAttachmentConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}

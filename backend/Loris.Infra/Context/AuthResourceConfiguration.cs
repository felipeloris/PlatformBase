using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Entities;
using Loris.Common.EF.Context;

namespace Loris.Infra.Context
{
    public class AuthResourceConfiguration : IEntityTypeConfiguration<AuthResource>
    {
        public void Configure(EntityTypeBuilder<AuthResource> entity)
        {
            entity.ToTable("auth_resource", "public")
                .HasKey(k => k.Id)
                .HasName("auth_resource_pk");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(e => e.Code)
                .HasColumnName("code")
                .HasMaxLength(5)
                .IsRequired()
                .IsUnicode(false);

            entity.HasIndex(i => i.Code)
                .IsUnique()
                .HasDatabaseName("auth_resource_ix_code");

            entity.Property(e => e.Dictionary)
                .HasColumnName("dictionary")
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(500)
                .IsUnicode(false);

            AuditRegisterConfiguration.Configure(entity);
        }
    }
}

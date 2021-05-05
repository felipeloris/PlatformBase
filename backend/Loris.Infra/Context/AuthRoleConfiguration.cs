using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Common.EF.Context;
using Loris.Entities;

namespace Loris.Infra.Context
{
    public class AuthRoleConfiguration : IEntityTypeConfiguration<AuthRole>
    {
        public void Configure(EntityTypeBuilder<AuthRole> entity)
        {
            entity.ToTable("auth_role", "public")
                .HasKey(k => k.Id)
                .HasName("auth_role_pk");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsUnicode(false);

            AuditRegisterConfiguration.Configure(entity);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Loris.Common.EF.Context;
using Loris.Entities;

namespace Loris.Infra.Context
{
    public class AuthRoleResourceConfiguration : IEntityTypeConfiguration<AuthRoleResource>
    {
        public void Configure(EntityTypeBuilder<AuthRoleResource> entity)
        {
            entity.ToTable("auth_role_resource", "public")
                .HasKey(k => new { k.AuthRoleId, k.AuthResourceId })
                .HasName("auth_role_resource_pk");

            entity.Property(e => e.AuthRoleId)
                .HasColumnName("auth_role_id")
                .IsRequired();

            entity.HasOne(d => d.AuthRole)
                .WithMany(p => p.AuthRoleResource)
                .HasForeignKey(d => d.AuthRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_role_resource_fk_auth_role");

            entity.Property(e => e.AuthResourceId)
                .HasColumnName("auth_resource_id")
                .IsRequired();

            entity.HasOne(d => d.AuthResource)
                .WithMany(p => p.AuthRoleResource)
                .HasForeignKey(d => d.AuthResourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auth_role_resource_fk_auth_resource");

            entity.Property(e => e.Permissions)
                .HasColumnName("permissions")
                .HasConversion<short>();

            AuditRegisterConfiguration.Configure(entity);
        }
    }
}

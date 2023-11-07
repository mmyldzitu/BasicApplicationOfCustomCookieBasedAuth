using CustomCookieBased.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomCookieBased.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Password).HasMaxLength(200).IsRequired();
            builder.Property(x => x.userName).HasMaxLength(250).IsRequired();
        }

        public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
        {
            public void Configure(EntityTypeBuilder<AppRole> builder)
            {
                builder.Property(x => x.Definition).HasMaxLength(200).IsRequired();
            }

        }
        public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
        {
            public void Configure(EntityTypeBuilder<AppUserRole> builder)
            {
                builder.HasKey(x => new { x.userID, x.roleId });
                builder.HasOne(x => x.AppRole).WithMany(y => y.userRoles).HasForeignKey(x => x.roleId);
                builder.HasOne(x => x.AppUser).WithMany(x => x.userRoles).HasForeignKey(x => x.userID);

            }
        }
    }
}

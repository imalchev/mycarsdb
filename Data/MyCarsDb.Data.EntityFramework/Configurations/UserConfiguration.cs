﻿namespace MyCarsDb.Data.EntityFramework.Configurations
{    
    using System.Data.Entity.ModelConfiguration;

    using MyCarsDb.Data.Models;

    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            //HasMany(x => x.Roles)
            //    .WithMany(x => x.Users)
            //    .Map(x =>
            //    {
            //        x.ToTable("UsersToRoles");
            //        x.MapLeftKey("UserId");
            //        x.MapRightKey("RoleId");
            //    });

            //Property(x => x.PasswordHash)
            //    .HasColumnName("PasswordHash")
            //    .HasColumnType("nvarchar")
            //    .IsMaxLength()
            //    .IsOptional();

            //Property(x => x.SecurityStamp)
            //    .HasColumnName("SecurityStamp")
            //    .HasColumnType("nvarchar")
            //    .IsMaxLength()
            //    .IsOptional();

            //Property(x => x.UserName)
            //    .HasColumnName("UserName")
            //    .HasColumnType("nvarchar")
            //    .HasMaxLength(256)
            //    .IsRequired();

            //HasMany(x => x.UserToRoles)
            //    .WithMany(x => x.Users)
            //    .Map(x =>
            //    {
            //        x.ToTable("UserRole");
            //        x.MapLeftKey("UserId");
            //        x.MapRightKey("RoleId");
            //    });

            //HasMany(x => x.Claims)
            //    .WithRequired(x => x.User)
            //    .HasForeignKey(x => x.UserId);

            //HasMany(x => x.Logins)
            //    .WithRequired(x => x.User)
            //    .HasForeignKey(x => x.UserId);
        }
    }
}
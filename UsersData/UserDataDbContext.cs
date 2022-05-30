using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UsersData.Entities;

namespace UsersData
{
    public class UserDataDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Role> Roles { get; set; }

        public UserDataDbContext(DbContextOptions<UserDataDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>()
                .HasKey(x => new { x.PermissionId, x.RoleId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Role)
                .WithMany(x => x.RolePermissions);
            modelBuilder.Entity<RolePermission>()
                .HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles);

            this.SeedInitialData(modelBuilder);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Description = "Add User", PermissonName = "AddUser", Id = 1 },
                new Permission { Description = "Delete User", PermissonName = "DeleteUser", Id = 2 },
                new Permission { Description = "Suspend User", PermissonName = "SuspendUser", Id = 3 },
                new Permission { Description = "Resource Getter", PermissonName = "ResourceGetter", Id = 4 },
                new Permission { Description = "Recipes Getter", PermissonName = "RecipesGetter", Id = 5 },
                new Permission { Description = "Crafted Items Getter", PermissonName = "CraftedItemsGetter", Id = 6 }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Description = "Admin", RoleName = "Admin" },
                new Role { Id = 2, Description = "User", RoleName = "User" }
                );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { RoleId = 1, PermissionId = 1 },
                new RolePermission { RoleId = 1, PermissionId = 2 },
                new RolePermission { RoleId = 1, PermissionId = 3 },
                new RolePermission { RoleId = 1, PermissionId = 4 },
                new RolePermission { RoleId = 1, PermissionId = 5 },
                new RolePermission { RoleId = 1, PermissionId = 6 },
                new RolePermission { RoleId = 2, PermissionId = 4 },
                new RolePermission { RoleId = 2, PermissionId = 5 },
                new RolePermission { RoleId = 2, PermissionId = 6 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "test", PasswordHash = "CGQZyJtqLb+SpBRZY7Qizlq8WCxPvBHd8yXmwFbREXM=" },
                new User { Id = 2, UserName = "user", PasswordHash = "fWfPYPXu3abY1WjJiBsE5m9vWk7F/RodQTkD0czSTKs=" }
                );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id= 1, RoleId = 1, UserId = 1},
                new UserRole { Id = 2, RoleId = 2, UserId = 2}
                );



        }
    }
}

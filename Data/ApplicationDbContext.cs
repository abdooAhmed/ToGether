using GpProject.Models;
using GpProject.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GpProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Report> Reports { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CommentReply> commentReplies { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
             
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("User", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserCliams", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleCliams", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");


            
            // one-to-many relationship between Book and BookAuthor
            builder.Entity<Comment>()
            .HasOne(ba => ba.User)
            .WithMany(b => b.Comments)
            .HasForeignKey(ba => ba.UserId);
            // one-to-many relationship between Author and BookAuthor
            builder.Entity<Comment>()
            .HasOne(ba => ba.Report)
            .WithMany(a => a.Comments)
            .HasForeignKey(ba => ba.ReportId);

            builder.Entity<CommentReply>()
            .HasOne(ba => ba.Comment)
            .WithMany(a => a.CommentReplies)
            .HasForeignKey(ba => ba.CommentId);
        }
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager =serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string AdminRole = "Admin";
            string PersonRole = "Person";
            string PoliceStationRole = "PoliceStation";
            await roleManager.CreateAsync(new IdentityRole(AdminRole));
            await roleManager.CreateAsync(new IdentityRole(PersonRole));
            await roleManager.CreateAsync(new IdentityRole(PoliceStationRole));
        }
    }
}

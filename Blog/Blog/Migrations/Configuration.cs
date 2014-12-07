using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Migrations
{
    public class Configuration : DbMigrationsConfiguration<Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Blog.Models.ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole()
                {
                    Name = "Admin"
                });
            }

            if (!userManager.Users.Any(usr => usr.Email == "yan.savitski1@gmail.com"))
            {
                var user = new ApplicationUser()
                {
                    Email = "yan.savitski1@gmail.com",
                    EmailConfirmed = true,
                    UserName = "yan.savitski1@gmail.com"
                };

                var result = userManager.Create(user, "!Q2w3e");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.Roles.Any(r => r.Name == "User"))
            {
                roleManager.Create(new IdentityRole()
                {
                    Name = "User"
                });
            }

            if (!userManager.Users.Any(usr => usr.Email == "test@gmail.com"))
            {
                var user = new ApplicationUser()
                {
                    Email = "test@gmail.com",
                    EmailConfirmed = true,
                    UserName = "test@gmail.com"
                };

                var result = userManager.Create(user, "!Q2w3e2");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                }
            }
            //base.Seed(context);
        }
    }
}
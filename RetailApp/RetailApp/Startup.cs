using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RetailApp.Models;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(RetailApp.Startup))]
namespace RetailApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
        }

        private void CreateRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

            }

            //if (!context.Users.Any(u => u.UserName == "admin1"))
            //{
            //    var store = new UserStore<ApplicationUser>(context);
            //    var manager = new UserManager<ApplicationUser>(store);
            //    var user = new ApplicationUser { UserName = "admin1", Email = "admin1@gmail.com" };

            //    manager.Create(user, "P@ssword1");
            //    manager.AddToRole(user.Id, "Admin");
            //}

            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole
                {
                    Name = "Employee"
                };
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole
                {
                    Name = "Manager"
                };
                roleManager.Create(role);

            }
            //Admin will POST, PUT, or DELETE from API data if company discontinues carrying certain items, prices update, or carries additional products
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

            }

        }
    }
}

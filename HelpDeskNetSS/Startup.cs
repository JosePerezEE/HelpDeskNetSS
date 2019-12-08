using HelpDeskNetSS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpDeskNetSS.Startup))]
namespace HelpDeskNetSS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Usuario"))
            {
                //create super admin role
                var role = new IdentityRole("Usuario");
                roleManager.Create(role);


                //create default user
                var user = new ApplicationUser();
                user.UserName = "escotodelmy88@gmail.com";
                user.Email = "escotodelmy88@gmail.com";
                string pwd = "Password@2017";

                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Usuario");
                }

            }
        }
    }
}

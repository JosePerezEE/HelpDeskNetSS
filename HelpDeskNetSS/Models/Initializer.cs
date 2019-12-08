using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace HelpDeskNetSS.Models
//{
//    public class Initializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
//    {
//        protected override void Seed(ApplicationDbContext context)
//        {

//            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
//            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
//            //Agregar Usuario Administrador
//            if (!roleManager.RoleExists("SuperAdmin"))
//            {
//                var role = new IdentityRole("Adminin");

//            roleManager.Create()
//            var AdminUser = new ApplicationUser
//            {
//                UserName = "admin12@admin.com",
//                Email = "admin12@admin.com",
//                EmailConfirmed = true
//            };
//            var newUser = userManager.Create(AdminUser, "Qwerty1234!");
//            if (newUser.Succeeded)
//            {
//                userManager.AddToRole(AdminUser.Id, "Admin");
//            }
//            context.SaveChanges();
//        }
//    }

//    public class ApplicationUserStore : UserStore<ApplicationUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUserStore<ApplicationUser>, IUserStore<ApplicationUser, string>, IDisposable
//    {
//        public ApplicationUserStore(ApplicationDbContext context)
//            : base(context)
//        {
//        }
//    }
//}
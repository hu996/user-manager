using Domain.Constants;
using Domain.Entity;
using Infarstructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domain.Entity.Helprs;

namespace Infarstructure.Seeds
{
    public static class DefaultUser
    {
        public static async Task SeedSuperAdminUserAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                Name="SuperAdmin",
                Email="SuperAdmin@domain.com",
                UserName="SuperAdmin@domain.com",
                ImageUser="",
                ActiveUser=true,
                EmailConfirmed=true,
               
                
            };
            var User = await userManager.FindByEmailAsync(DefaultUser.Email);
            if (User == null)
            {
                await userManager.CreateAsync(DefaultUser, "P@ssw0rd");
                await userManager.AddToRoleAsync(DefaultUser, "SuperAdmin");
                //To Add MoreThan one Role 
                //await roleManager.AddRolesAsync(DefaultUser,new List<string> { "SuperAdmin","Admin","Basic"});
            }
            // Add Claims 
            await roleManager.SeedClaimsAsync();
        }
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                Name = "User",
                Email = "Basic@domain.com",
                UserName = "Basic@domain.com",
                ImageUser = "",
                ActiveUser = true,
                EmailConfirmed = true,


            };
            var User = await userManager.FindByEmailAsync(DefaultUser.Email);
            if (User == null)
            {
                await userManager.CreateAsync(DefaultUser, "P@ssw0rd");
                await userManager.AddToRoleAsync(DefaultUser, "User");
                //To Add MoreThan one Role 
                //await roleManager.AddRolesAsync(DefaultUser,new List<string> { "SuperAdmin","Admin","Basic"});
            }
        }

        public static async Task SeedClaimsAsync(this RoleManager<IdentityRole>roleManager)
        {
            var adminrole = await roleManager.FindByNameAsync("SuperAdmin");
            var Modules = Enum.GetValues(typeof(PersmissionName));
            foreach(var module in Modules)
            {
                await roleManager.AddPermissionClaimAsync(adminrole, module.ToString());
            }
           
        }
        public static async Task AddPermissionClaimAsync(this RoleManager<IdentityRole>roleManager,IdentityRole role,string Module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermission = Persmissions.GeneratePermessionFromModule(Module);
            foreach(var permission in allPermission)
            {
                if(!allClaims.Any(x=>x.Type=="Permission" && x.Value== permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}


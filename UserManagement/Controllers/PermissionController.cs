using Domain.Constants;
using Infarstructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    public class PermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public PermissionController(RoleManager<IdentityRole>roleManager)
        {
            _roleManager = roleManager; 
        }
        public async Task<IActionResult> Index(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var cliams =  _roleManager.GetClaimsAsync(role).Result.Select(x=>x.Value).ToList();
            var allPermissions = Persmissions.PermissionList();
            var x = allPermissions.Select(x => new RoleClaimViewModel { Value = x }).ToList();
            foreach (var permission in allPermissions)
            {
                //if (cliams.Any(x => x == permission.Value))
                //{
                //    permission.Selcted = true;
                //}
            }
            return View(new PermissionViewModel
            {
                RoleId = roleId,
                RoleName=role.Name,
                //RoleClaims=allPermissions
            });
        }
    }
}

using Infarstructure.Data;
using Infarstructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebBook.Areas.Admin.Controllers
{

    [Authorize]
    public class AccountsController : Controller
    {

        #region Declaration
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbcontext _context;
        #endregion

        #region Constractur
        public AccountsController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser>userManager,
           ApplicationDbcontext context, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            
        }
        #endregion

        #region 
        public IActionResult Roles()
        {
            var model = new RolesViewModel
            {
              
                newrole = new newrole(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList()

            };
            return View(model);
        }
        [HttpPost]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Roles(RolesViewModel model)
        {

            var role = new IdentityRole
            {
                Id = model.newrole.RoleId,
                Name = model.newrole.RoleName
            };
            //Create Role
            if (role.Id == null)
            {
                role.Id = Guid.NewGuid().ToString();
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    //success
                   
                    HttpContext.Session.SetString("MessageType","Success");
                    HttpContext.Session.SetString("Title", "Saved Successfully");
                    HttpContext.Session.SetString("Message", "you have Created a new role");
                   
                }
                else
                {
                    //not success
                    HttpContext.Session.SetString("MessageType", "Error");
                    HttpContext.Session.SetString("Title", "Faild To Save");
                    HttpContext.Session.SetString("Message", "Cant save");
                }
            }
            //Update Role
            else
            {
                var roleupdate=_roleManager.Roles.FirstOrDefault(x=>x.Id==role.Id);
                roleupdate.Id = model.newrole.RoleId;
                roleupdate.Name = model.newrole.RoleName;
                var result = await _roleManager.UpdateAsync(roleupdate);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("MessageType", "Success");
                    HttpContext.Session.SetString("Title", "Edited Successfully");
                    HttpContext.Session.SetString("Message", "you have Edit a new role");
                }
                else
                {
                    HttpContext.Session.SetString("MessageType", "Error");
                    HttpContext.Session.SetString("Title", "Faild To Edit Role");
                    HttpContext.Session.SetString("Message", "Cant Edit");
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var result = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
          await  _roleManager.DeleteAsync(result);
           
            return RedirectToAction("Roles");
        }

        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                newregister = new NewRegister(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
                //Users = _context.VwUsers.OrderBy(x => x.Role).ToList()
                Users = _userManager.Users.OrderBy(x => x.ActiveUser).ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var filestreame = new FileStream(Path.Combine(@"wwwroot/Images", ImageName), FileMode.Create);
                    file[0].CopyTo(filestreame);
                    model.newregister.ImageUser = ImageName;
                }
                else
                {
                    model.newregister.ImageUser = model.newregister.ImageUser;
                }
                var user = new ApplicationUser
                {
                    Id=model.newregister.ID,
                    Name = model.newregister.Name,
                    UserName = model.newregister.Email,
                    Email= model.newregister.Email,
                    ActiveUser= model.newregister.IsActiveUser,
                    ImageUser= model.newregister.ImageUser

                };
                if (user.Id == null)
                {
                    //crete a new user
                    user.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(user, model.newregister.Password);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("MessageType", "Success");
                        HttpContext.Session.SetString("Title", "User Created Successfully");
                        HttpContext.Session.SetString("Message", "you have Addes a new User");
                        //user had registerd successfully 
                        //var role = _userManager.AddToRoleAsync(user, model.newregister.RoleName);
                        //if (role.IsCompletedSuccessfully)
                        //{
                        //    HttpContext.Session.SetString("MessageType", "Success");
                        //    HttpContext.Session.SetString("Title", "User Created Successfully");
                        //    HttpContext.Session.SetString("Message", "you have Addes a new User");
                        //}
                        //else
                        //{
                        //    HttpContext.Session.SetString("MessageType", "Error");
                        //    HttpContext.Session.SetString("Title", "Faild To Craete new user");
                        //    HttpContext.Session.SetString("Message", "Cant Create user");
                        //}
                    }
                    else
                    {
                        //user not registed
                        HttpContext.Session.SetString("MessageType", "Error");
                        HttpContext.Session.SetString("Title", "Faild To Craete new user");
                        HttpContext.Session.SetString("Message", "Cant Create user");
                       
                    }
                }
                else
                {
                    // update user
                    var userupdate = await _userManager.FindByIdAsync(user.Id);
                    userupdate.Id = model.newregister.ID;
                    userupdate.Name = model.newregister.Name;
                    userupdate.UserName = model.newregister.Email;
                    userupdate.Email = model.newregister.Email;
                    userupdate.ActiveUser = model.newregister.IsActiveUser;
                    userupdate.ImageUser = model.newregister.ImageUser;
                    var result =await _userManager.UpdateAsync(userupdate);

                    if (result.Succeeded)
                    {
                        // success
                        var oldrole = await _userManager.GetRolesAsync(userupdate);
                        await _userManager.RemoveFromRolesAsync(userupdate, oldrole);
                        var addrole = await _userManager.AddToRoleAsync(userupdate, model.newregister.Name);
                        if (addrole.Succeeded)
                        {
                            HttpContext.Session.SetString("MessageType", "Success");
                            HttpContext.Session.SetString("Title", "Edited Successfully");
                            HttpContext.Session.SetString("Message", "you have Edit a new user");
                        }
                        else
                        {
                            HttpContext.Session.SetString("MessageType", "Error");
                            HttpContext.Session.SetString("Title", "Faild To Edit user");
                            HttpContext.Session.SetString("Message", "Cant Edit");
                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("MessageType", "Error");
                        HttpContext.Session.SetString("Title", "Faild To Edit user");
                        HttpContext.Session.SetString("Message", "Cant Edit");
                    }


                }
            }
            return RedirectToAction("Register");
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string Id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(RegisterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.ChangePassword.Id);
            await _userManager.RemovePasswordAsync(user);
            var AddNewPassword = await _userManager.AddPasswordAsync(user, model.ChangePassword.NewPassword);
            if (AddNewPassword.Succeeded)
            {
                HttpContext.Session.SetString("MessageType", "Success");
                HttpContext.Session.SetString("Title", "Chamgerd Successfully");
                HttpContext.Session.SetString("Message", "you have Changed your Password");
            }
            else
            {
                HttpContext.Session.SetString("MessageType", "Error");
                HttpContext.Session.SetString("Title", "Faild To Cahnge password");
                HttpContext.Session.SetString("Message", "Cant Change Password");
            }

            return RedirectToAction("Register");
        }
        public async Task<IActionResult> DeleteUser(string Id)
        {
          var result=_userManager.Users.FirstOrDefault(x=>x.Id==Id);
            if (result.ImageUser != null&& result.ImageUser!=Guid.Empty.ToString())
            {
                var pathimage = Path.Combine(@"wwwroot/Images", result.ImageUser);
                if (System.IO.File.Exists(pathimage))
                {
                  System.IO.File.Delete(pathimage);
                }
            }
           if(( await _userManager.DeleteAsync(result)).Succeeded)
            {
                return RedirectToAction("Register");
            }

            return RedirectToAction("Register");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult>Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else{
                    ViewBag.ErrorLogin = false;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut(LoginViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }
    #endregion
}

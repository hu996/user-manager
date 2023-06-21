using Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.ViewModel
{
    public class RegisterViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public NewRegister newregister { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public ChangePasswordViewModel ChangePassword { get; set; }
    }
    public class NewRegister
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string ImageUser { get; set; }
        public bool IsActiveUser { get; set; }
        public string Password { get; set; }
        public string CompareUser { get; set; }
    }
}

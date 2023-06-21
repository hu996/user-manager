using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.ViewModel
{
    public class RolesViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public newrole newrole { get; set; }
    }
    public class newrole
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}

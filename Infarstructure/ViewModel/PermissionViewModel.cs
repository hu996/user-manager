using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.ViewModel
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; }=string.Empty;
        
        public string RoleName { get; set;} = string.Empty;
        public List<RoleClaimViewModel> RoleClaims { get; set; }=new List<RoleClaimViewModel>();
    }
    public class RoleClaimViewModel
    {
        public string Value { get; set; } = string.Empty;
        public bool Selected { get; set; }
    }
}

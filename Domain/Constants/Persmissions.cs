using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public static class Persmissions
    {
        public static List<String>GeneratePermessionFromModule(string Module)
        {
            return new List<string>
            {
                "$permession.{Module}.View",
                "$permession.{Module}.Create",
                "$permession.{Module}.Edit",
                "$permession.{Module}.Delete",

        };
            
        }
        public static List<string> PermissionList()
        {
            var allPermissions = new List<string>();
            
                foreach(var module in Enum.GetValues(typeof(Helprs.PersmissionName)))
            {
                allPermissions.AddRange(GeneratePermessionFromModule(module.ToString()));
            }

            return allPermissions;

        }

    }
}

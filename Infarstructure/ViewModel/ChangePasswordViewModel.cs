using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword")]
        public string ComparePassword { get; set; }
    }
}

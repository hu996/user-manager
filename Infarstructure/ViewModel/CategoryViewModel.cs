using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstructure.ViewModel
{
    public class CategoryViewModel
    {
        public List<Category> category { get; set; }
        public List<LogCategory> logCategory { get; set; }
        public Category NewCategory { get; set; }
    }
}

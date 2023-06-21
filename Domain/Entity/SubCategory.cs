using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class SubCategory
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        public int CurrentStatus { get; set; }
    }
}

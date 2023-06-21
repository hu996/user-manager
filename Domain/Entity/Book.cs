using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Book
    {
        public Guid ID { get; set; }
        public string Name { get; set; }    
        public string Author { get; set; }
        public string ImageName { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public bool Publish { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        //public Guid SubCatId { get; set; }
        //[ForeignKey("SubCatId")]
        //public SubCategory subcategory { get; set; }

    }
}

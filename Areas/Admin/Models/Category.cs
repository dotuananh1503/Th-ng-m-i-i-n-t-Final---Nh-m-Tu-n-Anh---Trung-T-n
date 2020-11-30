using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Danh mục")]
        public string CategoryName { get; set; }

        [DisplayName("SL Sản phẩm")]
        public int CategoryCountProduct { get; set; }
        public List<Product> Products { get; set; }

        public Category()
        {
            CategoryCountProduct = 0;
        }
    }
}

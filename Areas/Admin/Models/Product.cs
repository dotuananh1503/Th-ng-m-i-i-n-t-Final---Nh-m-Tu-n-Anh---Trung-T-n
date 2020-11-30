using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Models
{
    public class Product
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Mã SP")]
        public string Code { get; set; }

        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Not null")]
        public string Name { get; set; }

        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        public Category Category { get; set; }
        [DisplayName("Mã danh mục")]
        [Required(ErrorMessage = "Not null")]
        public int CategoryId { get; set; }

       

        [DisplayName("Giá tiền")]
        [Required(ErrorMessage = "Not null")]
        public int Price { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Count view sp")]
        public int ProductCountView { get; set; }
    }
}

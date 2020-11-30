using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Models
{
    public class Customer
    {
        
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Not null")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [DisplayName("Họ tên")]
        [Required(ErrorMessage = "Not null")]
        public string FullName { get; set; }

      

        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Not null")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Not null")]
        public string Email { get; set; }

        [DisplayName("Tổng điểm tích lũy")]
        public int TotalPoint { get; set; }

        [DisplayName("Mức giảm giá")]
        public string DiscountPercent { get; set; }


        public List<Receipt> Receipts { get; set; }
    }
}

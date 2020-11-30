using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Models
{
    public class ContactModel
    {
        [Key]
        public int ContactID { get; set; }
        [DisplayName("Tên người liên hệ")]
        [Required]
        public string ContactName { get; set; }
        [DisplayName("Số điện thoại")]
        [Required]
        public string ContactPhone { get; set; }
        [DisplayName("Địa chỉ")]
        [Required]
        public string ContactAddress { get; set; }
        [DisplayName("Địa chỉ Email")]
        [Required]
        public string ContactEmail { get; set; }
        [DisplayName("Nội dung liên hệ")]
        [Required]
        public string ContactNote { get; set; }
    }
}

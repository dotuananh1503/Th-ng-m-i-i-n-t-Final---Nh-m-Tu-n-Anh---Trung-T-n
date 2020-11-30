using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Models
{
    public class Receipt
    {
        [Key]
        [DisplayName("Mã hóa đơn")]
        public int OrderId { get; set; }
        [Required]
        [DisplayName("Tên khách hàng")]
        public string CustomerName { get; set; }
        [DisplayName("Địa Chỉ")]
        [Required]
        public string Address { get; set; }
       
        [DisplayName("Điện thoại")]
        [Required]
        public string Phone { get; set; }
        [DisplayName("Ngày tạo hóa đơn")]
        public DateTime OrderDate { get; set; }
        public List<DetailReceipt> detailReceipts { get; set; }
    }
}

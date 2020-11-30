using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Models
{
    public class DetailReceipt
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Mã hóa đơn")]
        [Required(ErrorMessage = "Not null")]
        public int ReceiptId { get; set; }

        [DisplayName("Mã sản phẩm")]
        [Required(ErrorMessage = "Not null")]
        public int ProductId { get; set; }

        [DisplayName("Giá tiền")]
        [Required(ErrorMessage = "Not null")]
        public int Price { get; set; }

        [DisplayName("Số lượng")]
        [Required(ErrorMessage = "Not null")]
        public int Quantity { get; set; }

        [DisplayName("Thành tiền")]
        [Required(ErrorMessage = "Not null")]
        public int TotalCost { get; set; }

        [DisplayName("Điểm tích lũy")]
        public int Point { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }
        public Receipt Receipt { get; set; }
        public Product Product { get; set; }
    }
}

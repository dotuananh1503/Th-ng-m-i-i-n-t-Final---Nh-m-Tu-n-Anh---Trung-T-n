using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Huflix.Areas.Admin.Models
{
    public class Comment
    {
        [Key]
        [DisplayName("STT")]
        public int CommentId { get; set; }
       
        [Required]
        [DisplayName("Nội dung")]
        public string CommentDescription { get; set; }
        [Required]
        [DisplayName("Người comment")]
        public string CommentName { get; set; }
        [DisplayName("Thời gian comment")]
        public string CommentTime { get; set; }

        [DisplayName("Mã SP")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

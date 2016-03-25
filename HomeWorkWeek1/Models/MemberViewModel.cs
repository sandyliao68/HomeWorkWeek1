using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeWorkWeek1.Models
{
    public class MemberViewModel
    {
        [Required]
        public string  帳號 { get; set; }
        [Required]
        public string  密碼 { get; set; }
    }
}
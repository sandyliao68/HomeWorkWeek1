using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeWorkWeek1.Models
{
    public class 客戶聯絡人批次更新ViewModel
    {
        [Required]
        public int Id { get; set; }

       
        public string 職稱 { get; set; }

       
        public string 手機 { get; set; }

       
        public string 電話 { get; set; }
    }
}
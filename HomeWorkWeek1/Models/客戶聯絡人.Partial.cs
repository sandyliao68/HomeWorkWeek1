namespace HomeWorkWeek1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Linq;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人:IValidatableObject
    {
        
    //實作模型驗證
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
       {
           var db = new ClientEntities();

           if (this.Id == 0) 
            {
                //create
                //var data = db.客戶聯絡人.Where(p => p.Email == Email && p.客戶Id == 客戶Id && p.Id !=Id).AsEnumerable();           
                if (db.客戶聯絡人.Where(p => p.客戶Id == this.客戶Id && p.Email == this.Email).Any())
                {
                    yield return new ValidationResult("EMAIL 已存在",new string[] {"Email"});
                }
            }
           else
           {
               //update
               if (db.客戶聯絡人.Where(p => p.客戶Id == this.客戶Id &&  p.Id != this.Id && p.Email == this.Email).Any())
               {
                   yield return new ValidationResult("EMAIL 已存在", new string[] { "Email" });
               }
           }

           yield return ValidationResult.Success;
 	     
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress(ErrorMessage = "請輸入正確的電子信箱")]
      // [Remote("CheckEmailduplicate", "Validate", AdditionalFields = "客戶Id,Id", ErrorMessage = "此客戶的聯絡人，其 Email 不能重複")] 
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
       // [手機格式驗證(ErrorMessage = "請輸入正確的手機格式 (09XX-XXXXXX )")] 
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "請輸入正確的手機格式 (09XX-XXXXXX )")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}

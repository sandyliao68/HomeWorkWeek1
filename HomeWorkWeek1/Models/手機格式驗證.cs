using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HomeWorkWeek1.Models
{
    public class 手機格式驗證:DataTypeAttribute
    {

        public 手機格式驗證()  : base(DataType.Text)
        {
        }
        public override bool IsValid(object value)
        {           
            if (value == null)
                return true;
           
            if (value is String)               
                return Regex.IsMatch(value.ToString(),@"\d{4}-\d{6}");
            else
                return true;
        }

    }
}
using HomeWorkWeek1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkWeek1.Controllers
{
    public class ValidateController:Controller
    {
        //實作「客戶聯絡人」時，同一個客戶下的聯絡人，其 Email 不能重複。
        public JsonResult CheckEmailduplicate(string Email, int 客戶Id, int Id)
        {
            bool isValidate = false;
            ClientEntities db = new ClientEntities();          
            var data = db.客戶聯絡人.Where(p => p.Email == Email && p.客戶Id == 客戶Id && p.Id !=Id).AsEnumerable();           

            if (data.Count() >0)
                isValidate= false;
            else
                isValidate= true;
            return Json(isValidate, JsonRequestBehavior.AllowGet);
     
        }

    }
}
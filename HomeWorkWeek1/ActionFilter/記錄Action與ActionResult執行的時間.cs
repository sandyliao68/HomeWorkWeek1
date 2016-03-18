using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HomeWorkWeek1.ActionFilter
{
    public class 記錄Action與ActionResult執行的時間 : ActionFilterAttribute
    {
        public DateTime SDateTime;
        public DateTime EDateTime;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //可以用來記錄ACTION進入及結束時間的LOG

            SDateTime = DateTime.Now;
            // 紀錄開始時間
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // 紀錄結束時間
            // 計算執行時間

            EDateTime = DateTime.Now;
            TimeSpan exectuionTime = EDateTime.Subtract(SDateTime);
            filterContext.Controller.ViewBag.執行時間 = exectuionTime;
            Debug.WriteLine("執行時間" + exectuionTime);
            base.OnResultExecuted(filterContext);
        }
    }
}
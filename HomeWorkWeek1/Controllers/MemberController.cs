using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkWeek1.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
         [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
    }
}
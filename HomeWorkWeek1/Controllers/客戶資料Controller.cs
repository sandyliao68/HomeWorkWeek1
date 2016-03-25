using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWorkWeek1.Models;
using HomeWorkWeek1.ActionFilter;
using PagedList;

namespace HomeWorkWeek1.Controllers
{
    [記錄Action與ActionResult執行的時間]
    public class 客戶資料Controller : Controller
    {
        //改用 repo       
        //private ClientEntities db = new ClientEntities();     
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        // GET: 客戶資料
         public ActionResult Index(string keyword, string drpCategory,string sortOrder,int pageNo=1)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Value = "1", Text = "北區客戶" });
            items.Add(new SelectListItem() { Value = "2", Text = "中區客戶" });
            items.Add(new SelectListItem() { Value = "3", Text = "南區客戶" });
            ViewData["drpCategory"] = new SelectList(items, "Value", "Text");
           // var db = (ClientEntities)repo.UnitOfWork.Context;
            //SelectList selectList = new SelectList(CategoryList, "CategoryID", "CategoryName");
            //ViewBag.客戶分類 = selectList;
            var data = repo.Query(keyword, drpCategory);
           

            //return View(data);
            //* 修改「客戶資料列表」與「客戶聯絡人列表」頁面，設定讓每個欄位都能進行排序 (可升冪(DESC)、可降冪排序)
            //參考:http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
            ViewBag.客戶名稱SortParm = String.IsNullOrEmpty(sortOrder) ? "客戶名稱_desc" : "";  //初始排序方法
            ViewBag.統一編號SortParm = sortOrder == "統一編號" ? "統一編號_desc" : "統一編號";
            ViewBag.電話SortParm = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.傳真SortParm = sortOrder == "傳真" ? "傳真_desc" : "傳真";
            ViewBag.地址SortParm = sortOrder == "地址" ? "地址_desc" : "地址";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.客戶分類SortParm = sortOrder == "客戶分類" ? "客戶分類_desc" : "客戶分類";
            //data = from s in db.客戶資料
            //               select s;
             data= from s in data
                   select s;
            switch (sortOrder)
            {
                 //客戶名稱	統一編號	電話	傳真	地址	Email	客戶分類
                case "客戶名稱_desc":
                    data = data.OrderByDescending(s => s.客戶名稱);
                    break;
                case "統一編號":
                    data = data.OrderBy(s => s.統一編號);
                    break;
                case "統一編號_desc":
                    data = data.OrderByDescending(s => s.統一編號);
                    break;
                case "電話":
                    data = data.OrderBy(s => s.電話);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(s => s.電話);
                    break;
                case "傳真":
                    data = data.OrderBy(s => s.傳真);
                    break;
                case "傳真_desc":
                    data = data.OrderByDescending(s => s.傳真);
                    break;
                case "地址":
                    data = data.OrderBy(s => s.地址);
                    break;
                case "地址_desc":
                    data = data.OrderByDescending(s => s.地址);
                    break;
                case "Email":
                    data = data.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(s => s.Email);
                    break;
                case "客戶分類":
                    data = data.OrderBy(s => s.客戶分類);
                    break;
                case "客戶分類_desc":
                    data = data.OrderByDescending(s => s.客戶分類);
                    break;
                default:
                    data = data.OrderBy(s => s.客戶名稱);
                    break;
            }
            //分頁設定
      
            var pageData = data.ToPagedList(pageNo,5);
            //ViewBag.pageNo = pageNo;
            return View(pageData);

        }

        //[HttpPost]
        //public ActionResult Index(string ClientName)
        //{
        //    var data = db.客戶資料.OrderByDescending(p => p.Id).AsQueryable();
        //    if (!String.IsNullOrEmpty(ClientName))
        //    {
        //        data = data.Where(p => p.客戶名稱.Contains(ClientName));
        //    }
        //    return View(data);
        //}

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value );
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            //DropDownList 設定方法,使用IEnumerable<SelectListItem>"
            //http://kevintsengtw.blogspot.tw/2012/09/aspnet-mvc-3-dropdownlist.html
            
            //List<SelectListItem> items = new List<SelectListItem>();
            //foreach (var List in CategoryList)
            //{
            //    items.Add(new SelectListItem()
            //    {
            //        Text = List.CategoryName,
            //        Value = List.CategoryID
            //    });
            //}
            //ViewBag.客戶分類 = items;
            //客戶分類下拉選單清單


            //SelectList selectList = new SelectList(CategoryList, "CategoryID", "CategoryName");
            //ViewBag.客戶分類 = selectList;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Value = "", Text = "--請選擇--" });
            items.Add(new SelectListItem() { Value = "1", Text = "北區客戶" });
            items.Add(new SelectListItem() { Value = "2", Text = "中區客戶" });
            items.Add(new SelectListItem() { Value = "3", Text = "南區客戶" });
            ViewBag.客戶分類 = new SelectList(items, "Value", "Text");

            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,帳號,密碼")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                客戶資料.是否已刪除 = false;
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }           
            //SelectList selectList= new SelectList(CategoryList, "CategoryID", "CategoryName", 客戶資料.客戶分類);
            //ViewBag.客戶分類 = selectList;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Value = "", Text = "--請選擇--" });
            items.Add(new SelectListItem() { Value = "1", Text = "北區客戶" });
            items.Add(new SelectListItem() { Value = "2", Text = "中區客戶" });
            items.Add(new SelectListItem() { Value = "3", Text = "南區客戶" });
            ViewBag.Category = new SelectList(items, "Value", "Text", 客戶資料.客戶分類);
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類,帳號,密碼")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                var db = (ClientEntities)repo.UnitOfWork.Context;
                db.Entry(客戶資料).State = EntityState.Modified;
                客戶資料.是否已刪除 = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
           
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id);

            客戶資料.是否已刪除 = true;
           
            //db.客戶資料.Remove(客戶資料);
            //db.SaveChanges();
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (ClientEntities)repo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    }
}

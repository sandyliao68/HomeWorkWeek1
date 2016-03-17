using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWorkWeek1.Models;

namespace HomeWorkWeek1.Controllers
{
    public class 客戶資料Controller : Controller
    {
        //改用 repo       
        //private ClientEntities db = new ClientEntities();
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
         List<Category> CategoryList = new List<Category>()
            {
                new Category {CategoryID="", CategoryName="請選擇"},
                new Category {CategoryID="北區客戶", CategoryName="北區客戶"},
                new Category {CategoryID="中區客戶", CategoryName="中區客戶"},
                new Category {CategoryID="南區客戶", CategoryName="南區客戶"},            
            };
       

        // GET: 客戶資料
         public ActionResult Index(string keyword, string drpCategory)
        {

            var data = repo.Query(keyword, drpCategory);
            return View(data);

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
           

            SelectList selectList = new SelectList(CategoryList, "CategoryID", "CategoryName");
            ViewBag.客戶分類 = selectList;

            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
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
            SelectList selectList= new SelectList(CategoryList, "CategoryID", "CategoryName", 客戶資料.客戶分類);
            ViewBag.客戶分類 = selectList;
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
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

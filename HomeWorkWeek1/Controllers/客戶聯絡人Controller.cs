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

namespace HomeWorkWeek1.Controllers
{
     [記錄Action與ActionResult執行的時間]
    public class 客戶聯絡人Controller : Controller
    {
        //private ClientEntities db = new ClientEntities();
        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();
       
        // GET: 客戶聯絡人
        public ActionResult Index(string PName, string TitleName, string sortOrder, int?  CId)
        {          
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料).Where(客 => 客.客戶資料.是否已刪除 == false);       
            //return View(客戶聯絡人.ToList());
            //var data = repo.Query(PName, TitleName);
            //return View(data);
          
            //SORT
            var db = (ClientEntities)repo.UnitOfWork.Context;
            ViewBag.客戶名稱SortParm = String.IsNullOrEmpty(sortOrder) ? "客戶名稱_desc" : "";  //初始排序方法
            ViewBag.職稱SortParm = sortOrder == "職稱" ? "職稱_desc" : "職稱";
            ViewBag.姓名SortParm = sortOrder == "姓名" ? "姓名_desc" : "姓名";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.手機SortParm = sortOrder == "手機" ? "手機_desc" : "手機";
            ViewBag.電話SortParm = sortOrder == "電話" ? "電話_desc" : "電話";

            var data = from s in db.客戶聯絡人
                         select s;
            if (!String.IsNullOrEmpty(PName))
            {
                data = data.Where(s => s.姓名.Contains(PName));
            }
            if (!String.IsNullOrEmpty(TitleName))
            {
                data = data.Where(s => s.職稱.Contains(TitleName));
            }
            if (CId !=null)
            {
                data = data.Where(s => s.客戶Id == CId);
            }
               
           
            switch (sortOrder)
            {
                //客戶名稱	統一編號	電話	傳真	地址	Email	客戶分類
                case "客戶名稱_desc":
                    data = data.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                case "職稱":
                    data = data.OrderBy(s => s.職稱);
                    break;
                case "職稱_desc":
                    data = data.OrderByDescending(s => s.職稱);
                    break;
                case "姓名":
                    data = data.OrderBy(s => s.姓名);
                    break;
                case "姓名_desc":
                    data = data.OrderByDescending(s => s.姓名);
                    break;
                case "Email":
                    data = data.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(s => s.Email);
                    break;
                case "手機":
                    data = data.OrderBy(s => s.手機);
                    break;
                case "手機_desc":
                    data = data.OrderByDescending(s => s.手機);
                    break;
                case "電話":
                    data = data.OrderBy(s => s.電話);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(s => s.電話);
                    break;
               
                default:
                    data = data.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
            }
            return View(data.ToList());          
          
        }

        //[HttpPost]
        //public ActionResult Index(string PName, string TitleName)
        //{
        //    var data = repo.Query(PName, TitleName);
        //    return View(data);         
        //}
        [HttpPost]       
        public ActionResult Index(IList<客戶聯絡人批次更新ViewModel> data)
        {
             List<客戶聯絡人> 回傳聯絡人 = new List<客戶聯絡人>();
             if (ModelState.IsValid)
             {
                 foreach (var item in data)
                 {
                     var 客戶聯絡人 = repo.Find(item.Id);
                     客戶聯絡人.職稱 = item.職稱;
                     客戶聯絡人.手機 = item.手機;
                     客戶聯絡人.電話 = item.電話;
                     回傳聯絡人.Add(客戶聯絡人);
                 }
                 try
                 {
                     repo.UnitOfWork.Commit();
                     TempData["EditDoneMsg"] = "批次更新成功!";    //用完一次就清空
                 }
                 catch (Exception ex)
                 {
                     throw;
                 }
                 
             }
             return View(回傳聯絡人);    //重新回到輸入錯誤的狀態
        }


        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            var db = (ClientEntities)repo.UnitOfWork.Context;
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                客戶聯絡人.是否已刪除 = false;
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
               // db.SaveChanges();
                return RedirectToAction("Index");
            }
            var db = (ClientEntities)repo.UnitOfWork.Context;            
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            var db = (ClientEntities)repo.UnitOfWork.Context;
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        public ActionResult Edit(int Id, FormCollection form)   // FormCollection form-->沒用到只是為了不重複Edit
        {
            //延遲驗證
            客戶聯絡人 客戶聯絡人 = repo.Find(Id);   //從DB取得完整資料
            if (TryUpdateModel<客戶聯絡人>(客戶聯絡人, new string[] {
                 "Id","職稱","手機","電話" })) //BIND部份屬性(有提供修改的欄位)
            {
                repo.UnitOfWork.Commit();
            }
            TempData["EditDoneMsg"] = "更新成功!";    //用完一次就清空
            return RedirectToAction("Index");
            //var db = (ClientEntities)repo.UnitOfWork.Context;
            //if (ModelState.IsValid)
            //{
            //    db.Entry(客戶聯絡人).State = EntityState.Modified;
            //    客戶聯絡人.是否已刪除 = false;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            //return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
            客戶聯絡人.是否已刪除 = true;
            repo.UnitOfWork.Commit();
            //db.客戶聯絡人.Remove(客戶聯絡人);
           // db.SaveChanges();
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

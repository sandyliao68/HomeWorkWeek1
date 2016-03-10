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
    public class vwClientsController : Controller
    {
        private ClientEntities db = new ClientEntities();

        // GET: vwClients
        public ActionResult Index()
        {
            return View(db.vwClient.ToList());
        }

        // GET: vwClients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwClient vwClient = db.vwClient.Find(id);
            if (vwClient == null)
            {
                return HttpNotFound();
            }
            return View(vwClient);
        }

        // GET: vwClients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vwClients/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] vwClient vwClient)
        {
            if (ModelState.IsValid)
            {
                db.vwClient.Add(vwClient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vwClient);
        }

        // GET: vwClients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwClient vwClient = db.vwClient.Find(id);
            if (vwClient == null)
            {
                return HttpNotFound();
            }
            return View(vwClient);
        }

        // POST: vwClients/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] vwClient vwClient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vwClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vwClient);
        }

        // GET: vwClients/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vwClient vwClient = db.vwClient.Find(id);
            if (vwClient == null)
            {
                return HttpNotFound();
            }
            return View(vwClient);
        }

        // POST: vwClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            vwClient vwClient = db.vwClient.Find(id);
            db.vwClient.Remove(vwClient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

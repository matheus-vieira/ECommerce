using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Front.Contexts;
using Front.Models;

namespace Front.Controllers
{
    public class SellsController : Controller
    {
        private ECommerceDbContext db = new ECommerceDbContext();

        // GET: Sells
        public ActionResult Index()
        {
            return View(db.Sells.ToList());
        }

        // GET: Sells/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sell sell = db.Sells
                .Include("SellItems.Product")
                .FirstOrDefault(s => s.SellId == id.Value);
            if (sell == null)
            {
                return HttpNotFound();
            }
            return View(sell);
        }

        // GET: Sells/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sells/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellId,SellNumber,Date,BuyerName,BuyerDoc,PhoneNumber,TotalPrice")] Sell sell)
        {
            if (ModelState.IsValid)
            {
                sell.Date = DateTime.Now;
                sell.SellNumber = db.Sells.Max(s => s.SellNumber);
                if (!sell.SellNumber.HasValue)
                    sell.SellNumber = 2000;
                db.Sells.Add(sell);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = sell.SellId });
            }

            return View(sell);
        }

        // GET: Sells/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sell sell = db.Sells
                .Include("SellItems.Product")
                .FirstOrDefault(s => s.SellId == id.Value);

            if (sell == null)
            {
                return HttpNotFound();
            }

            if(sell.Closed)
                return RedirectToAction("Details", "Sells", new { id = sell.SellId });

            ViewBag.ProductId = new SelectList(db.Products.Where(p => !p.Deleted), "ProductId", "Name");
            ViewBag.SellId = new SelectList(db.Sells, "SellId", "SellNumber");
            return View(sell);
        }

        // POST: Sells/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellId,BuyerName,Closed,BuyerDoc,PhoneNumber")] Sell sell)
        {
            if (ModelState.IsValid)
            {
                var updated = db.Sells.Find(sell.SellId);
                updated.BuyerName = sell.BuyerName;
                updated.BuyerDoc = sell.BuyerDoc;
                updated.PhoneNumber = sell.PhoneNumber;
                if (!updated.Closed)
                    updated.Closed = sell.Closed;
                db.Entry(updated).State = EntityState.Modified;
                db.SaveChanges();

                if (updated.Closed)
                    return RedirectToAction("Index");

                return RedirectToAction("Edit", "Sells", new { id = sell.SellId });
            }
            return RedirectToAction("Edit", "Sells", new { id = sell.SellId });
        }

        // GET: Sells/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sell sell = db.Sells.Find(id);
            if (sell == null)
            {
                return HttpNotFound();
            }

            if (sell.Closed)
                return RedirectToAction("Details", "Sells", new { id = sell.SellId });

            return View(sell);
        }

        // POST: Sells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Sell sell = db.Sells.Find(id);

            db.SellItems
                .Where(si => si.SellId == id)
                .ToList()
                .ForEach(si => db.SellItems.Remove(si));

            db.Sells.Remove(sell);
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

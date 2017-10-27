using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FishInABox.Models.DataModel;

namespace FishInABox.Controllers
{
    public class TANKsController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: TANKs
        public async Task<ActionResult> Index()
        {
            var tANKs = db.TANKs.Include(t => t.TANK_BAY);
            return View(await tANKs.ToListAsync());
        }

        // GET: TANKs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK tANK = await db.TANKs.FindAsync(id);
            if (tANK == null)
            {
                return HttpNotFound();
            }
            return View(tANK);
        }

        // GET: TANKs/Create
        public ActionResult Create()
        {
            ViewBag.BAY_FK = new SelectList(db.TANK_BAY, "ID_PK", "ID_CODE");
            return View();
        }

        // POST: TANKs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,BAY_FK,ID_CODE,TEXT,RFID")] TANK tANK)
        {
            if (ModelState.IsValid)
            {
                db.TANKs.Add(tANK);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BAY_FK = new SelectList(db.TANK_BAY, "ID_PK", "ID_CODE", tANK.BAY_FK);
            return View(tANK);
        }

        // GET: TANKs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK tANK = await db.TANKs.FindAsync(id);
            if (tANK == null)
            {
                return HttpNotFound();
            }
            ViewBag.BAY_FK = new SelectList(db.TANK_BAY, "ID_PK", "ID_CODE", tANK.BAY_FK);
            return View(tANK);
        }

        // POST: TANKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,BAY_FK,ID_CODE,TEXT,RFID")] TANK tANK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tANK).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BAY_FK = new SelectList(db.TANK_BAY, "ID_PK", "ID_CODE", tANK.BAY_FK);
            return View(tANK);
        }

        // GET: TANKs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK tANK = await db.TANKs.FindAsync(id);
            if (tANK == null)
            {
                return HttpNotFound();
            }
            return View(tANK);
        }

        // POST: TANKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TANK tANK = await db.TANKs.FindAsync(id);
            db.TANKs.Remove(tANK);
            await db.SaveChangesAsync();
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

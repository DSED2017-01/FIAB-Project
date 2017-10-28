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
    public class TANK_LOG_DAILYController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: TANK_LOG_DAILY
        public async Task<ActionResult> Index()
        {
            var tANK_LOG_DAILY = db.TANK_LOG_DAILY.Include(t => t.REASON_MORTALITY).Include(t => t.TANK_LOG).Include(t => t.SYS_STUFF);
            return View(await tANK_LOG_DAILY.ToListAsync());
        }

        // GET: TANK_LOG_DAILY/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_LOG_DAILY tANK_LOG_DAILY = await db.TANK_LOG_DAILY.FindAsync(id);
            if (tANK_LOG_DAILY == null)
            {
                return HttpNotFound();
            }
            return View(tANK_LOG_DAILY);
        }

        // GET: TANK_LOG_DAILY/Create
        public ActionResult Create()
        {
            ViewBag.REASON_FK = new SelectList(db.REASON_MORTALITY, "ID_PK", "ID_CODE");
            ViewBag.LOG_FK = new SelectList(db.TANK_LOG, "ID_PK", "SPECIES_TEXT");
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE");
            return View();
        }

        // POST: TANK_LOG_DAILY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,LOG_FK,REASON_FK,QTY,COMMENT,STUFF_FK,LOG_DATE")] TANK_LOG_DAILY tANK_LOG_DAILY)
        {
            if (ModelState.IsValid)
            {
                db.TANK_LOG_DAILY.Add(tANK_LOG_DAILY);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.REASON_FK = new SelectList(db.REASON_MORTALITY, "ID_PK", "ID_CODE", tANK_LOG_DAILY.REASON_FK);
            ViewBag.LOG_FK = new SelectList(db.TANK_LOG, "ID_PK", "SPECIES_TEXT", tANK_LOG_DAILY.LOG_FK);
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE", tANK_LOG_DAILY.STUFF_FK);
            return View(tANK_LOG_DAILY);
        }

        // GET: TANK_LOG_DAILY/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_LOG_DAILY tANK_LOG_DAILY = await db.TANK_LOG_DAILY.FindAsync(id);
            if (tANK_LOG_DAILY == null)
            {
                return HttpNotFound();
            }
            ViewBag.REASON_FK = new SelectList(db.REASON_MORTALITY, "ID_PK", "ID_CODE", tANK_LOG_DAILY.REASON_FK);
            ViewBag.LOG_FK = new SelectList(db.TANK_LOG, "ID_PK", "SPECIES_TEXT", tANK_LOG_DAILY.LOG_FK);
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE", tANK_LOG_DAILY.STUFF_FK);
            return View(tANK_LOG_DAILY);
        }

        // POST: TANK_LOG_DAILY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,LOG_FK,REASON_FK,QTY,COMMENT,STUFF_FK,LOG_DATE")] TANK_LOG_DAILY tANK_LOG_DAILY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tANK_LOG_DAILY).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.REASON_FK = new SelectList(db.REASON_MORTALITY, "ID_PK", "ID_CODE", tANK_LOG_DAILY.REASON_FK);
            ViewBag.LOG_FK = new SelectList(db.TANK_LOG, "ID_PK", "SPECIES_TEXT", tANK_LOG_DAILY.LOG_FK);
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE", tANK_LOG_DAILY.STUFF_FK);
            return View(tANK_LOG_DAILY);
        }

        // GET: TANK_LOG_DAILY/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_LOG_DAILY tANK_LOG_DAILY = await db.TANK_LOG_DAILY.FindAsync(id);
            if (tANK_LOG_DAILY == null)
            {
                return HttpNotFound();
            }
            return View(tANK_LOG_DAILY);
        }

        // POST: TANK_LOG_DAILY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TANK_LOG_DAILY tANK_LOG_DAILY = await db.TANK_LOG_DAILY.FindAsync(id);
            db.TANK_LOG_DAILY.Remove(tANK_LOG_DAILY);
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

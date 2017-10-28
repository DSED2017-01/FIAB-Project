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
    public class TANK_LOGController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: TANK_LOG
        public async Task<ActionResult> Index()
        {
            var tANK_LOG = db.TANK_LOG.Include(t => t.MOVEMENT_PERIOD).Include(t => t.TANK).Include(t => t.SYS_STUFF).Include(t => t.MARINE_SPECIES).Include(t => t.RECORD_PET_SIZE);
            return View(await tANK_LOG.ToListAsync());
        }

        // GET: TANK_LOG/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_LOG tANK_LOG = await db.TANK_LOG.FindAsync(id);
            if (tANK_LOG == null)
            {
                return HttpNotFound();
            }
            return View(tANK_LOG);
        }

        // GET: TANK_LOG/Create
        public ActionResult Create()
        {
            ViewBag.PERIOD_FK = new SelectList(db.MOVEMENT_PERIOD, "ID_PK", "TEXT");
            ViewBag.TANK_FK = new SelectList(db.TANKs, "ID_PK", "ID_CODE");
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE");
            ViewBag.SPECIES_FK = new SelectList(db.MARINE_SPECIES, "ID_PK", "SCIENTIFIC");
            ViewBag.SIZE_FK = new SelectList(db.RECORD_PET_SIZE, "ID_PK", "DESCRIPTION");
            return View();
        }

        // POST: TANK_LOG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,PERIOD_FK,TANK_FK,SPECIES_FK,SPECIES_TEXT,SPECIES_TEXT_2,QTY,COMMENT,STUFF_FK,ORDER_FK,SIZE_FK")] TANK_LOG tANK_LOG)
        {
            if (ModelState.IsValid)
            {
                db.TANK_LOG.Add(tANK_LOG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PERIOD_FK = new SelectList(db.MOVEMENT_PERIOD, "ID_PK", "TEXT", tANK_LOG.PERIOD_FK);
            ViewBag.TANK_FK = new SelectList(db.TANKs, "ID_PK", "ID_CODE", tANK_LOG.TANK_FK);
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE", tANK_LOG.STUFF_FK);
            ViewBag.SPECIES_FK = new SelectList(db.MARINE_SPECIES, "ID_PK", "SCIENTIFIC", tANK_LOG.SPECIES_FK);
            ViewBag.SIZE_FK = new SelectList(db.RECORD_PET_SIZE, "ID_PK", "DESCRIPTION", tANK_LOG.SIZE_FK);
            return View(tANK_LOG);
        }

        // GET: TANK_LOG/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_LOG tANK_LOG = await db.TANK_LOG.FindAsync(id);
            if (tANK_LOG == null)
            {
                return HttpNotFound();
            }
            ViewBag.PERIOD_FK = new SelectList(db.MOVEMENT_PERIOD, "ID_PK", "TEXT", tANK_LOG.PERIOD_FK);
            ViewBag.TANK_FK = new SelectList(db.TANKs, "ID_PK", "ID_CODE", tANK_LOG.TANK_FK);
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE", tANK_LOG.STUFF_FK);
            ViewBag.SPECIES_FK = new SelectList(db.MARINE_SPECIES, "ID_PK", "SCIENTIFIC", tANK_LOG.SPECIES_FK);
            ViewBag.SIZE_FK = new SelectList(db.RECORD_PET_SIZE, "ID_PK", "DESCRIPTION", tANK_LOG.SIZE_FK);
            return View(tANK_LOG);
        }

        // POST: TANK_LOG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,PERIOD_FK,TANK_FK,SPECIES_FK,SPECIES_TEXT,SPECIES_TEXT_2,QTY,COMMENT,STUFF_FK,ORDER_FK,SIZE_FK")] TANK_LOG tANK_LOG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tANK_LOG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PERIOD_FK = new SelectList(db.MOVEMENT_PERIOD, "ID_PK", "TEXT", tANK_LOG.PERIOD_FK);
            ViewBag.TANK_FK = new SelectList(db.TANKs, "ID_PK", "ID_CODE", tANK_LOG.TANK_FK);
            ViewBag.STUFF_FK = new SelectList(db.SYS_STUFF, "ID_PK", "ID_CODE", tANK_LOG.STUFF_FK);
            ViewBag.SPECIES_FK = new SelectList(db.MARINE_SPECIES, "ID_PK", "SCIENTIFIC", tANK_LOG.SPECIES_FK);
            ViewBag.SIZE_FK = new SelectList(db.RECORD_PET_SIZE, "ID_PK", "DESCRIPTION", tANK_LOG.SIZE_FK);
            return View(tANK_LOG);
        }

        // GET: TANK_LOG/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_LOG tANK_LOG = await db.TANK_LOG.FindAsync(id);
            if (tANK_LOG == null)
            {
                return HttpNotFound();
            }
            return View(tANK_LOG);
        }

        // POST: TANK_LOG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TANK_LOG tANK_LOG = await db.TANK_LOG.FindAsync(id);
            db.TANK_LOG.Remove(tANK_LOG);
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

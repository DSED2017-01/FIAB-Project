using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using FishInABox.Models;
using FishInABox.Models.DataModel;

namespace FishInABox.Controllers
{
    public class MARINE_SPECIESController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: MARINE_SPECIES
        public async Task<ActionResult> Index()
        {
            /* https://ole.michelsen.dk/blog/grouping-data-with-linq-and-mvc.html  */
            var mARINE_SPECIES = db.MARINE_SPECIES.Include(m => m.MARINE_CLASS).Include(m => m.MARINE_FAMILY);
            var speciesGRouped = from s in mARINE_SPECIES
                                 group s by s.MARINE_CLASS into g
                                 select new GroupModel<MARINE_SPECIES, MARINE_CLASS> { Key = g.Key, Values = g };
            //return View(await mARINE_SPECIES.ToListAsync());
            return View(await speciesGRouped.ToListAsync());
        }

        // GET: MARINE_SPECIES/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_SPECIES mARINE_SPECIES = await db.MARINE_SPECIES.FindAsync(id);
            if (mARINE_SPECIES == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Create
        public ActionResult Create()
        {
            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT");
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT");
            return View();
        }

        // POST: MARINE_SPECIES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,CLASS_FK,SPECIES_FK,SCIENTIFIC,COMMON,TEXT,FLAG,FAMILY_FK")] MARINE_SPECIES mARINE_SPECIES)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_SPECIES.Add(mARINE_SPECIES);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT", mARINE_SPECIES.CLASS_FK);
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT", mARINE_SPECIES.FAMILY_FK);
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_SPECIES mARINE_SPECIES = await db.MARINE_SPECIES.FindAsync(id);
            if (mARINE_SPECIES == null)
            {
                return HttpNotFound();
            }
            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT", mARINE_SPECIES.CLASS_FK);
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT", mARINE_SPECIES.FAMILY_FK);
            return View(mARINE_SPECIES);
        }

        // POST: MARINE_SPECIES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,CLASS_FK,SPECIES_FK,SCIENTIFIC,COMMON,TEXT,FLAG,FAMILY_FK")] MARINE_SPECIES mARINE_SPECIES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_SPECIES).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CLASS_FK = new SelectList(db.MARINE_CLASS, "ID_PK", "TEXT", mARINE_SPECIES.CLASS_FK);
            ViewBag.FAMILY_FK = new SelectList(db.MARINE_FAMILY, "ID_PK", "TEXT", mARINE_SPECIES.FAMILY_FK);
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_SPECIES mARINE_SPECIES = await db.MARINE_SPECIES.FindAsync(id);
            if (mARINE_SPECIES == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_SPECIES);
        }

        // POST: MARINE_SPECIES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MARINE_SPECIES mARINE_SPECIES = await db.MARINE_SPECIES.FindAsync(id);
            db.MARINE_SPECIES.Remove(mARINE_SPECIES);
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

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
    public class MARINE_FAMILYController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: MARINE_FAMILY
        public async Task<ActionResult> Index()
        {
            return View(await db.MARINE_FAMILY.ToListAsync());
        }

        // GET: MARINE_FAMILY/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_FAMILY mARINE_FAMILY = await db.MARINE_FAMILY.FindAsync(id);
            if (mARINE_FAMILY == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_FAMILY);
        }

        // GET: MARINE_FAMILY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MARINE_FAMILY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,TEXT,SCHEDULE3,FLAG")] MARINE_FAMILY mARINE_FAMILY)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_FAMILY.Add(mARINE_FAMILY);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mARINE_FAMILY);
        }

        // GET: MARINE_FAMILY/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_FAMILY mARINE_FAMILY = await db.MARINE_FAMILY.FindAsync(id);
            if (mARINE_FAMILY == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_FAMILY);
        }

        // POST: MARINE_FAMILY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,TEXT,SCHEDULE3,FLAG")] MARINE_FAMILY mARINE_FAMILY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_FAMILY).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mARINE_FAMILY);
        }

        // GET: MARINE_FAMILY/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_FAMILY mARINE_FAMILY = await db.MARINE_FAMILY.FindAsync(id);
            if (mARINE_FAMILY == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_FAMILY);
        }

        // POST: MARINE_FAMILY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MARINE_FAMILY mARINE_FAMILY = await db.MARINE_FAMILY.FindAsync(id);
            db.MARINE_FAMILY.Remove(mARINE_FAMILY);
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

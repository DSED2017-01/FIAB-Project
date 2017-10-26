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
    public class MARINE_CLASSController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: MARINE_CLASS
        public async Task<ActionResult> Index()
        {
            return View(await db.MARINE_CLASS.ToListAsync());
        }

        // GET: MARINE_CLASS/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLASS mARINE_CLASS = await db.MARINE_CLASS.FindAsync(id);
            if (mARINE_CLASS == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLASS);
        }

        // GET: MARINE_CLASS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MARINE_CLASS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,TEXT,SCHEDULE4,FLAG")] MARINE_CLASS mARINE_CLASS)
        {
            if (ModelState.IsValid)
            {
                db.MARINE_CLASS.Add(mARINE_CLASS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mARINE_CLASS);
        }

        // GET: MARINE_CLASS/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLASS mARINE_CLASS = await db.MARINE_CLASS.FindAsync(id);
            if (mARINE_CLASS == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLASS);
        }

        // POST: MARINE_CLASS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,TEXT,SCHEDULE4,FLAG")] MARINE_CLASS mARINE_CLASS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mARINE_CLASS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mARINE_CLASS);
        }

        // GET: MARINE_CLASS/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MARINE_CLASS mARINE_CLASS = await db.MARINE_CLASS.FindAsync(id);
            if (mARINE_CLASS == null)
            {
                return HttpNotFound();
            }
            return View(mARINE_CLASS);
        }

        // POST: MARINE_CLASS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MARINE_CLASS mARINE_CLASS = await db.MARINE_CLASS.FindAsync(id);
            db.MARINE_CLASS.Remove(mARINE_CLASS);
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

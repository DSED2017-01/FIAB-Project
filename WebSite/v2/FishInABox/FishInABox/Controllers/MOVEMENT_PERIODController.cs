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
    public class MOVEMENT_PERIODController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: MOVEMENT_PERIOD
        public async Task<ActionResult> Index()
        {
            return View(await db.MOVEMENT_PERIOD.ToListAsync());
        }

        // GET: MOVEMENT_PERIOD/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVEMENT_PERIOD mOVEMENT_PERIOD = await db.MOVEMENT_PERIOD.FindAsync(id);
            if (mOVEMENT_PERIOD == null)
            {
                return HttpNotFound();
            }
            return View(mOVEMENT_PERIOD);
        }

        // GET: MOVEMENT_PERIOD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MOVEMENT_PERIOD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,START_DATE,TEXT,CLOSED_DATE,CLOSED_FLAG")] MOVEMENT_PERIOD mOVEMENT_PERIOD)
        {
            if (ModelState.IsValid)
            {
                db.MOVEMENT_PERIOD.Add(mOVEMENT_PERIOD);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mOVEMENT_PERIOD);
        }

        // GET: MOVEMENT_PERIOD/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVEMENT_PERIOD mOVEMENT_PERIOD = await db.MOVEMENT_PERIOD.FindAsync(id);
            if (mOVEMENT_PERIOD == null)
            {
                return HttpNotFound();
            }
            return View(mOVEMENT_PERIOD);
        }

        // POST: MOVEMENT_PERIOD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,START_DATE,TEXT,CLOSED_DATE,CLOSED_FLAG")] MOVEMENT_PERIOD mOVEMENT_PERIOD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mOVEMENT_PERIOD).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mOVEMENT_PERIOD);
        }

        // GET: MOVEMENT_PERIOD/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOVEMENT_PERIOD mOVEMENT_PERIOD = await db.MOVEMENT_PERIOD.FindAsync(id);
            if (mOVEMENT_PERIOD == null)
            {
                return HttpNotFound();
            }
            return View(mOVEMENT_PERIOD);
        }

        // POST: MOVEMENT_PERIOD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MOVEMENT_PERIOD mOVEMENT_PERIOD = await db.MOVEMENT_PERIOD.FindAsync(id);
            db.MOVEMENT_PERIOD.Remove(mOVEMENT_PERIOD);
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

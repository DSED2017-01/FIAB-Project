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
    public class REASON_MORTALITYController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: REASON_MORTALITY
        public async Task<ActionResult> Index()
        {
            return View(await db.REASON_MORTALITY.ToListAsync());
        }

        // GET: REASON_MORTALITY/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REASON_MORTALITY rEASON_MORTALITY = await db.REASON_MORTALITY.FindAsync(id);
            if (rEASON_MORTALITY == null)
            {
                return HttpNotFound();
            }
            return View(rEASON_MORTALITY);
        }

        // GET: REASON_MORTALITY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: REASON_MORTALITY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,ID_CODE,TEXT")] REASON_MORTALITY rEASON_MORTALITY)
        {
            if (ModelState.IsValid)
            {
                db.REASON_MORTALITY.Add(rEASON_MORTALITY);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rEASON_MORTALITY);
        }

        // GET: REASON_MORTALITY/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REASON_MORTALITY rEASON_MORTALITY = await db.REASON_MORTALITY.FindAsync(id);
            if (rEASON_MORTALITY == null)
            {
                return HttpNotFound();
            }
            return View(rEASON_MORTALITY);
        }

        // POST: REASON_MORTALITY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,ID_CODE,TEXT")] REASON_MORTALITY rEASON_MORTALITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEASON_MORTALITY).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rEASON_MORTALITY);
        }

        // GET: REASON_MORTALITY/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REASON_MORTALITY rEASON_MORTALITY = await db.REASON_MORTALITY.FindAsync(id);
            if (rEASON_MORTALITY == null)
            {
                return HttpNotFound();
            }
            return View(rEASON_MORTALITY);
        }

        // POST: REASON_MORTALITY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            REASON_MORTALITY rEASON_MORTALITY = await db.REASON_MORTALITY.FindAsync(id);
            db.REASON_MORTALITY.Remove(rEASON_MORTALITY);
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

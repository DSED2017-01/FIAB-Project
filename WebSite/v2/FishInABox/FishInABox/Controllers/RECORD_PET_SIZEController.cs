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
    public class RECORD_PET_SIZEController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: RECORD_PET_SIZE
        public async Task<ActionResult> Index()
        {
            return View(await db.RECORD_PET_SIZE.ToListAsync());
        }

        // GET: RECORD_PET_SIZE/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECORD_PET_SIZE rECORD_PET_SIZE = await db.RECORD_PET_SIZE.FindAsync(id);
            if (rECORD_PET_SIZE == null)
            {
                return HttpNotFound();
            }
            return View(rECORD_PET_SIZE);
        }

        // GET: RECORD_PET_SIZE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RECORD_PET_SIZE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,DESCRIPTION")] RECORD_PET_SIZE rECORD_PET_SIZE)
        {
            if (ModelState.IsValid)
            {
                db.RECORD_PET_SIZE.Add(rECORD_PET_SIZE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rECORD_PET_SIZE);
        }

        // GET: RECORD_PET_SIZE/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECORD_PET_SIZE rECORD_PET_SIZE = await db.RECORD_PET_SIZE.FindAsync(id);
            if (rECORD_PET_SIZE == null)
            {
                return HttpNotFound();
            }
            return View(rECORD_PET_SIZE);
        }

        // POST: RECORD_PET_SIZE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,DESCRIPTION")] RECORD_PET_SIZE rECORD_PET_SIZE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rECORD_PET_SIZE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rECORD_PET_SIZE);
        }

        // GET: RECORD_PET_SIZE/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECORD_PET_SIZE rECORD_PET_SIZE = await db.RECORD_PET_SIZE.FindAsync(id);
            if (rECORD_PET_SIZE == null)
            {
                return HttpNotFound();
            }
            return View(rECORD_PET_SIZE);
        }

        // POST: RECORD_PET_SIZE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RECORD_PET_SIZE rECORD_PET_SIZE = await db.RECORD_PET_SIZE.FindAsync(id);
            db.RECORD_PET_SIZE.Remove(rECORD_PET_SIZE);
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

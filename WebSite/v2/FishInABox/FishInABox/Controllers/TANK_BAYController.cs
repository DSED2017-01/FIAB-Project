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
    public class TANK_BAYController : Controller
    {
        private fishinaboxEntities db = new fishinaboxEntities();

        // GET: TANK_BAY
        public async Task<ActionResult> Index()
        {
            return View(await db.TANK_BAY.ToListAsync());
        }

        // GET: TANK_BAY/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_BAY tANK_BAY = await db.TANK_BAY.FindAsync(id);
            if (tANK_BAY == null)
            {
                return HttpNotFound();
            }
            return View(tANK_BAY);
        }

        // GET: TANK_BAY/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TANK_BAY/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID_PK,ID_CODE,TEXT")] TANK_BAY tANK_BAY)
        {
            if (ModelState.IsValid)
            {
                db.TANK_BAY.Add(tANK_BAY);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tANK_BAY);
        }

        // GET: TANK_BAY/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_BAY tANK_BAY = await db.TANK_BAY.FindAsync(id);
            if (tANK_BAY == null)
            {
                return HttpNotFound();
            }
            return View(tANK_BAY);
        }

        // POST: TANK_BAY/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID_PK,ID_CODE,TEXT")] TANK_BAY tANK_BAY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tANK_BAY).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tANK_BAY);
        }

        // GET: TANK_BAY/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TANK_BAY tANK_BAY = await db.TANK_BAY.FindAsync(id);
            if (tANK_BAY == null)
            {
                return HttpNotFound();
            }
            return View(tANK_BAY);
        }

        // POST: TANK_BAY/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TANK_BAY tANK_BAY = await db.TANK_BAY.FindAsync(id);
            db.TANK_BAY.Remove(tANK_BAY);
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

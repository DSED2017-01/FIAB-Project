using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FishInABox.Models.DataModel;

namespace FishInABox.Controllers
{
    public class MARINE_SPECIESController : Controller
    {
        private readonly FishInABoxContext _context;

        public MARINE_SPECIESController(FishInABoxContext context)
        {
            _context = context;
        }

        // GET: MARINE_SPECIES
        public async Task<IActionResult> Index()
        {
            var fishInABoxContext = _context.MARINE_SPECIES.Include(m => m.ClassFkNavigation).Include(m => m.FamilyFkNavigation);
            return View(await fishInABoxContext.ToListAsync());
        }

        // GET: MARINE_SPECIES/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mARINE_SPECIES = await _context.MARINE_SPECIES
                .Include(m => m.ClassFkNavigation)
                .Include(m => m.FamilyFkNavigation)
                .SingleOrDefaultAsync(m => m.IdPk == id);
            if (mARINE_SPECIES == null)
            {
                return NotFound();
            }

            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Create
        public IActionResult Create()
        {
            ViewData["ClassFk"] = new SelectList(_context.MARINE_CLASS, "IdPk", "Schedule4");
            ViewData["FamilyFk"] = new SelectList(_context.MARINE_FAMILY, "IdPk", "Schedule3");
            return View();
        }

        // POST: MARINE_SPECIES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPk,ClassFk,SpeciesFk,Scientific,Common,Text,Flag,FamilyFk")] MARINE_SPECIES mARINE_SPECIES)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mARINE_SPECIES);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassFk"] = new SelectList(_context.MARINE_CLASS, "IdPk", "Schedule4", mARINE_SPECIES.ClassFk);
            ViewData["FamilyFk"] = new SelectList(_context.MARINE_FAMILY, "IdPk", "Schedule3", mARINE_SPECIES.FamilyFk);
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mARINE_SPECIES = await _context.MARINE_SPECIES.SingleOrDefaultAsync(m => m.IdPk == id);
            if (mARINE_SPECIES == null)
            {
                return NotFound();
            }
            ViewData["ClassFk"] = new SelectList(_context.MARINE_CLASS, "IdPk", "Schedule4", mARINE_SPECIES.ClassFk);
            ViewData["FamilyFk"] = new SelectList(_context.MARINE_FAMILY, "IdPk", "Schedule3", mARINE_SPECIES.FamilyFk);
            return View(mARINE_SPECIES);
        }

        // POST: MARINE_SPECIES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPk,ClassFk,SpeciesFk,Scientific,Common,Text,Flag,FamilyFk")] MARINE_SPECIES mARINE_SPECIES)
        {
            if (id != mARINE_SPECIES.IdPk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mARINE_SPECIES);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MARINE_SPECIESExists(mARINE_SPECIES.IdPk))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassFk"] = new SelectList(_context.MARINE_CLASS, "IdPk", "Schedule4", mARINE_SPECIES.ClassFk);
            ViewData["FamilyFk"] = new SelectList(_context.MARINE_FAMILY, "IdPk", "Schedule3", mARINE_SPECIES.FamilyFk);
            return View(mARINE_SPECIES);
        }

        // GET: MARINE_SPECIES/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mARINE_SPECIES = await _context.MARINE_SPECIES
                .Include(m => m.ClassFkNavigation)
                .Include(m => m.FamilyFkNavigation)
                .SingleOrDefaultAsync(m => m.IdPk == id);
            if (mARINE_SPECIES == null)
            {
                return NotFound();
            }

            return View(mARINE_SPECIES);
        }

        // POST: MARINE_SPECIES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mARINE_SPECIES = await _context.MARINE_SPECIES.SingleOrDefaultAsync(m => m.IdPk == id);
            _context.MARINE_SPECIES.Remove(mARINE_SPECIES);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MARINE_SPECIESExists(int id)
        {
            return _context.MARINE_SPECIES.Any(e => e.IdPk == id);
        }
    }
}

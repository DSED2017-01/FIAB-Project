using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using FishInABox.Models;
using FishInABox.Models.DataModel;

namespace FishInABox.Controllers
{
    public class MarineSpeciesController : Controller
    {
        private readonly fishinaboxContext _context;

        public MarineSpeciesController(fishinaboxContext context)
        {
            _context = context;
        }

        // GET: MarineSpecies
        public async Task<IActionResult> Index()
        {
            var fishinaboxContext = _context.MarineSpecies.Include(m => m.ClassFkNavigation).Include(m => m.FamilyFkNavigation);

            return View(await fishinaboxContext.ToListAsync());
        }

        // GET: MarineSpecies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marineSpecies = await _context.MarineSpecies
                .Include(m => m.ClassFkNavigation)
                .Include(m => m.FamilyFkNavigation)
                .SingleOrDefaultAsync(m => m.IdPk == id);
            if (marineSpecies == null)
            {
                return NotFound();
            }

            return View(marineSpecies);
        }

        // GET: MarineSpecies/Create
        public IActionResult Create()
        {
            ViewData["ClassFk"] = new SelectList(_context.MarineClass, "IdPk", "Schedule4");
            ViewData["FamilyFk"] = new SelectList(_context.MarineFamily, "IdPk", "Schedule3");
            return View();
        }

        // POST: MarineSpecies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPk,ClassFk,SpeciesFk,Scientific,Common,Text,Flag,FamilyFk")] MarineSpecies marineSpecies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marineSpecies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassFk"] = new SelectList(_context.MarineClass, "IdPk", "Schedule4", marineSpecies.ClassFk);
            ViewData["FamilyFk"] = new SelectList(_context.MarineFamily, "IdPk", "Schedule3", marineSpecies.FamilyFk);
            return View(marineSpecies);
        }

        // GET: MarineSpecies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marineSpecies = await _context.MarineSpecies.SingleOrDefaultAsync(m => m.IdPk == id);
            if (marineSpecies == null)
            {
                return NotFound();
            }
            ViewData["ClassFk"] = new SelectList(_context.MarineClass, "IdPk", "Schedule4", marineSpecies.ClassFk);
            ViewData["FamilyFk"] = new SelectList(_context.MarineFamily, "IdPk", "Schedule3", marineSpecies.FamilyFk);
            return View(marineSpecies);
        }

        // POST: MarineSpecies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPk,ClassFk,SpeciesFk,Scientific,Common,Text,Flag,FamilyFk")] MarineSpecies marineSpecies)
        {
            if (id != marineSpecies.IdPk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marineSpecies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarineSpeciesExists(marineSpecies.IdPk))
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
            ViewData["ClassFk"] = new SelectList(_context.MarineClass, "IdPk", "Schedule4", marineSpecies.ClassFk);
            ViewData["FamilyFk"] = new SelectList(_context.MarineFamily, "IdPk", "Schedule3", marineSpecies.FamilyFk);
            return View(marineSpecies);
        }

        // GET: MarineSpecies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marineSpecies = await _context.MarineSpecies
                .Include(m => m.ClassFkNavigation)
                .Include(m => m.FamilyFkNavigation)
                .SingleOrDefaultAsync(m => m.IdPk == id);
            if (marineSpecies == null)
            {
                return NotFound();
            }

            return View(marineSpecies);
        }

        // POST: MarineSpecies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marineSpecies = await _context.MarineSpecies.SingleOrDefaultAsync(m => m.IdPk == id);
            _context.MarineSpecies.Remove(marineSpecies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarineSpeciesExists(int id)
        {
            return _context.MarineSpecies.Any(e => e.IdPk == id);
        }
    }
}

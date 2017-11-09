using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FishInABox.Models.DataModel;

namespace FishInABox.Controllers
{
    [Produces("application/json")]
    [Route("api/MarineSpeciesAPI")]
    public class MarineSpeciesAPIController : Controller
    {
        private readonly fishinaboxContext _context;

        public MarineSpeciesAPIController(fishinaboxContext context)
        {
            _context = context;
        }

        // GET: api/MarineSpeciesAPI
        [HttpGet]
        public IEnumerable<MarineSpecies> GetMarineSpecies()
        {
            return _context.MarineSpecies;
        }

        // GET: api/MarineSpeciesAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarineSpecies([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marineSpecies = await _context.MarineSpecies.SingleOrDefaultAsync(m => m.IdPk == id);

            if (marineSpecies == null)
            {
                return NotFound();
            }

            return Ok(marineSpecies);
        }

        // PUT: api/MarineSpeciesAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarineSpecies([FromRoute] int id, [FromBody] MarineSpecies marineSpecies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != marineSpecies.IdPk)
            {
                return BadRequest();
            }

            _context.Entry(marineSpecies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarineSpeciesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MarineSpeciesAPI
        [HttpPost]
        public async Task<IActionResult> PostMarineSpecies([FromBody] MarineSpecies marineSpecies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MarineSpecies.Add(marineSpecies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarineSpecies", new { id = marineSpecies.IdPk }, marineSpecies);
        }

        // DELETE: api/MarineSpeciesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarineSpecies([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marineSpecies = await _context.MarineSpecies.SingleOrDefaultAsync(m => m.IdPk == id);
            if (marineSpecies == null)
            {
                return NotFound();
            }

            _context.MarineSpecies.Remove(marineSpecies);
            await _context.SaveChangesAsync();

            return Ok(marineSpecies);
        }

        private bool MarineSpeciesExists(int id)
        {
            return _context.MarineSpecies.Any(e => e.IdPk == id);
        }
    }
}
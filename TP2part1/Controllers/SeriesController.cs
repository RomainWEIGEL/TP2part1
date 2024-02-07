using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP2part1.Models.EntityFramework;

namespace TP2part1.Controllers
{
    public class SeriesController : Controller
    {
        private readonly Tp2part1Context _context;

        public SeriesController(Tp2part1Context context)
        {
            _context = context;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
              return _context.Series != null ? 
                          View(await _context.Series.ToListAsync()) :
                          Problem("Entity set 'Tp2part1Context.Series'  is null.");
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                .FirstOrDefaultAsync(m => m.Serieid == id);
            if (serie == null)
            {
                return NotFound();
            }

            return View(serie);
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Serieid,Titre,Resume,Nbsaisons,Nbepisodes,Anneecreation,Network")] Serie serie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serie);
        }

        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }

            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            return View(serie);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Serieid,Titre,Resume,Nbsaisons,Nbepisodes,Anneecreation,Network")] Serie serie)
        {
            if (id != serie.Serieid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SerieExists(serie.Serieid))
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
            return View(serie);
        }

        // GET: Series/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                .FirstOrDefaultAsync(m => m.Serieid == id);
            if (serie == null)
            {
                return NotFound();
            }

            // Remove the serie from the context
            _context.Series.Remove(serie);
            await _context.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return NoContentResult after successful deletion
        }


        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Series == null)
            {
                return Problem("Entity set 'Tp2part1Context.Series'  is null.");
            }
            var serie = await _context.Series.FindAsync(id);
            if (serie != null)
            {
                _context.Series.Remove(serie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SerieExists(int id)
        {
            return _context.Series.Any(e => e.Serieid == id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Serie>> GetSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            return serie;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            return await _context.Series.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Serie>> PostSerie(Serie serie)
        {
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSerie", new { id = serie.Serieid }, serie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSerie(int id, Serie serie)
        {
            if (id != serie.Serieid)
            {
                return BadRequest();
            }
            _context.Entry(serie).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(id))
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
        
    }
}

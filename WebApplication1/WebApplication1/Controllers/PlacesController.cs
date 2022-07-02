using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlacesController : Controller
    {
        private readonly TravelContext _context;

        public PlacesController(TravelContext context)
        {
            _context = context;
        }

        // GET: Places
        public IActionResult Index()
        {
              return _context.Place != null ? 
                          View(_context.Place) :
                          Problem("Entity set 'TravelContext.Place'  is null.");
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place
                .FirstOrDefaultAsync(m => m.PlaceId == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaceId,Name,Description,Tel,Add,Px,Py,Region,Town,Class,Opentime,Closetime,Timestay")] Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(place);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PlaceId,Name,Description,Tel,Add,Px,Py,Region,Town,Class,Opentime,Closetime,Timestay")] Place place)
        {
            if (id != place.PlaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.PlaceId))
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
            return View(place);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Place == null)
            {
                return NotFound();
            }

            var place = await _context.Place
                .FirstOrDefaultAsync(m => m.PlaceId == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Place == null)
            {
                return Problem("Entity set 'TravelContext.Place'  is null.");
            }
            var place = await _context.Place.FindAsync(id);
            if (place != null)
            {
                _context.Place.Remove(place);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(string id)
        {
          return (_context.Place?.Any(e => e.PlaceId == id)).GetValueOrDefault();
        }
    }
}

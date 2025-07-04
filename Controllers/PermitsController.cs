using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1stModule_PIPremises.Data;
using _1stModule_PIPremises.Models;

namespace _1stModule_PIPremises.Controllers
{
    public class PermitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Permits
        public async Task<IActionResult> Index(string stationFilter)
        {
            // Get distinct StationNames for dropdown
            var stationList = await _context.Permits
                .Select(p => p.StationName)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            ViewBag.Locations = new SelectList(stationList);

            // Filter by selected station
            var permits = from p in _context.Permits select p;

            if (!string.IsNullOrEmpty(stationFilter))
            {
                permits = permits.Where(p => p.StationName == stationFilter);
            }

            return View(await permits.ToListAsync());
        }

        // GET: Permits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var permit = await _context.Permits
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (permit == null)
                return NotFound();

            return View(permit);
        }

        // GET: Permits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Permits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PermitNumber,PermitType,IssueDateTime,FunctionalLocation,Description,StationName")] Permit permit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permit);
        }

        // GET: Permits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var permit = await _context.Permits.FindAsync(id);
            if (permit == null)
                return NotFound();

            return View(permit);
        }

        // POST: Permits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PermitNumber,PermitType,IssueDateTime,FunctionalLocation,Description,StationName")] Permit permit)
        {
            if (id != permit.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermitExists(permit.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(permit);
        }

        // GET: Permits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var permit = await _context.Permits
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (permit == null)
                return NotFound();

            return View(permit);
        }

        // POST: Permits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permit = await _context.Permits.FindAsync(id);
            if (permit != null)
                _context.Permits.Remove(permit);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermitExists(int id)
        {
            return _context.Permits.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceProject.Data;
using ECommerceProject.Models;

namespace ECommerceProject.Controllers
{
    public class ConfigarationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfigarationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Configarations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Configaration.ToListAsync());
        }

        // GET: Configarations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configaration = await _context.Configaration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configaration == null)
            {
                return NotFound();
            }

            return View(configaration);
        }

        // GET: Configarations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configarations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConfigarationName")] Configaration configaration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configaration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configaration);
        }

        // GET: Configarations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configaration = await _context.Configaration.FindAsync(id);
            if (configaration == null)
            {
                return NotFound();
            }
            return View(configaration);
        }

        // POST: Configarations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConfigarationName")] Configaration configaration)
        {
            if (id != configaration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configaration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigarationExists(configaration.Id))
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
            return View(configaration);
        }

        // GET: Configarations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configaration = await _context.Configaration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configaration == null)
            {
                return NotFound();
            }

            return View(configaration);
        }

        // POST: Configarations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configaration = await _context.Configaration.FindAsync(id);
            if (configaration != null)
            {
                _context.Configaration.Remove(configaration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigarationExists(int id)
        {
            return _context.Configaration.Any(e => e.Id == id);
        }
    }
}

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
    public class SaleDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaleDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SaleDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SaleDetails.Include(s => s.Product).Include(s => s.SalesOrder).Include(s => s.Unit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetails = await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.SalesOrder)
                .Include(s => s.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetails == null)
            {
                return NotFound();
            }

            return View(saleDetails);
        }

        // GET: SaleDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductCode");
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrder, "Id", "InvoiceNumber");
            ViewData["UnitId"] = new SelectList(_context.Set<Unit>(), "Id", "UnitName");
            return View();
        }

        // POST: SaleDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SalesOrderId,ProductId,UnitId,UnitPrice,SaleQuantity")] SaleDetails saleDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductCode", saleDetails.ProductId);
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrder, "Id", "InvoiceNumber", saleDetails.SalesOrderId);
            ViewData["UnitId"] = new SelectList(_context.Set<Unit>(), "Id", "UnitName", saleDetails.UnitId);
            return View(saleDetails);
        }

        // GET: SaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetails = await _context.SaleDetails.FindAsync(id);
            if (saleDetails == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductCode", saleDetails.ProductId);
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrder, "Id", "InvoiceNumber", saleDetails.SalesOrderId);
            ViewData["UnitId"] = new SelectList(_context.Set<Unit>(), "Id", "UnitName", saleDetails.UnitId);
            return View(saleDetails);
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SalesOrderId,ProductId,UnitId,UnitPrice,SaleQuantity")] SaleDetails saleDetails)
        {
            if (id != saleDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailsExists(saleDetails.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "ProductCode", saleDetails.ProductId);
            ViewData["SalesOrderId"] = new SelectList(_context.SalesOrder, "Id", "InvoiceNumber", saleDetails.SalesOrderId);
            ViewData["UnitId"] = new SelectList(_context.Set<Unit>(), "Id", "UnitName", saleDetails.UnitId);
            return View(saleDetails);
        }

        // GET: SaleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetails = await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.SalesOrder)
                .Include(s => s.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleDetails == null)
            {
                return NotFound();
            }

            return View(saleDetails);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleDetails = await _context.SaleDetails.FindAsync(id);
            if (saleDetails != null)
            {
                _context.SaleDetails.Remove(saleDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleDetailsExists(int id)
        {
            return _context.SaleDetails.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models.EfModels;

namespace WebApplication3.Controllers
{
    public class SupplierContactsController : Controller
    {
        private readonly ISpanDemoContext _context;

        public SupplierContactsController(ISpanDemoContext context)
        {
            _context = context;
        }

        // GET: SupplierContacts
        public async Task<IActionResult> Index()
        {
            var iSpanDemoContext = _context.SupplierContacts.Include(s => s.Supplier);
            return View(await iSpanDemoContext.ToListAsync());
        }

        // GET: SupplierContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierContact = await _context.SupplierContacts
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierContact == null)
            {
                return NotFound();
            }

            return View(supplierContact);
        }

        // GET: SupplierContacts/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CompanyName");
            return View();
        }

        // POST: SupplierContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SupplierId,ContactName,Email,Tel,IsPrimary")] SupplierContact supplierContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplierContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CompanyName", supplierContact.SupplierId);
            return View(supplierContact);
        }

        // GET: SupplierContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierContact = await _context.SupplierContacts.FindAsync(id);
            if (supplierContact == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CompanyName", supplierContact.SupplierId);
            return View(supplierContact);
        }

        // POST: SupplierContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SupplierId,ContactName,Email,Tel,IsPrimary")] SupplierContact supplierContact)
        {
            if (id != supplierContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplierContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierContactExists(supplierContact.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "CompanyName", supplierContact.SupplierId);
            return View(supplierContact);
        }

        // GET: SupplierContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplierContact = await _context.SupplierContacts
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplierContact == null)
            {
                return NotFound();
            }

            return View(supplierContact);
        }

        // POST: SupplierContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplierContact = await _context.SupplierContacts.FindAsync(id);
            if (supplierContact != null)
            {
                _context.SupplierContacts.Remove(supplierContact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierContactExists(int id)
        {
            return _context.SupplierContacts.Any(e => e.Id == id);
        }
    }
}

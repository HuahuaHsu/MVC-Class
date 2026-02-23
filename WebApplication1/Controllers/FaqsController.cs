using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.EfModels;

namespace WebApplication1.Controllers
{
    public class FaqsController : Controller
    {
        private readonly dbFirstAppContext _context;

        public FaqsController(dbFirstAppContext context)
        {
            _context = context;
        }

        // GET: Faqs
        public async Task<IActionResult> Index()
        {
            return View(await _context.DboFaqs.ToListAsync());
        }

        // GET: Faqs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dboFaq = await _context.DboFaqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dboFaq == null)
            {
                return NotFound();
            }

            return View(dboFaq);
        }

        // GET: Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DboFaq dboFaq)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dboFaq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dboFaq);
        }

        // GET: Faqs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dboFaq = await _context.DboFaqs.FindAsync(id);
            if (dboFaq == null)
            {
                return NotFound();
            }
            return View(dboFaq);
        }

        // POST: Faqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,DboFaq dboFaq)
        {
            if (id != dboFaq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dboFaq);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DboFaqExists(dboFaq.Id))
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
            return View(dboFaq);
        }

        // GET: Faqs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dboFaq = await _context.DboFaqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dboFaq == null)
            {
                return NotFound();
            }

            return View(dboFaq);
        }

        // POST: Faqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dboFaq = await _context.DboFaqs.FindAsync(id);
            if (dboFaq != null)
            {
                _context.DboFaqs.Remove(dboFaq);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DboFaqExists(int id)
        {
            return _context.DboFaqs.Any(e => e.Id == id);
        }
    }
}

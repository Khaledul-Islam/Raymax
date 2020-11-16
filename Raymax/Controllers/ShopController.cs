using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Raymax.Data;
using Raymax.Models;

namespace Raymax.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shop
        public async Task<IActionResult> Index()
        {
            return View(await _context.Terms.ToListAsync());
        }

        // GET: Shop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terms = await _context.Terms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (terms == null)
            {
                return NotFound();
            }

            return View(terms);
        }

        // GET: Shop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TermsANDPolicy")] Terms terms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(terms);
        }

        // GET: Shop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terms = await _context.Terms.FindAsync(id);
            if (terms == null)
            {
                return NotFound();
            }
            return View(terms);
        }

        // POST: Shop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TermsANDPolicy")] Terms terms)
        {
            if (id != terms.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermsExists(terms.ID))
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
            return View(terms);
        }

        // GET: Shop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terms = await _context.Terms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (terms == null)
            {
                return NotFound();
            }

            return View(terms);
        }

        // POST: Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terms = await _context.Terms.FindAsync(id);
            _context.Terms.Remove(terms);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TermsExists(int id)
        {
            return _context.Terms.Any(e => e.ID == id);
        }
    }
}

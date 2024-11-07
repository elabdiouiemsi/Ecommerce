using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class PanierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Panier
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Paniers.Include(p => p.Consomateur);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Panier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Paniers
                .Include(p => p.Consomateur)
                .FirstOrDefaultAsync(m => m.PanierId == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // GET: Panier/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Consomateurs, "UserId", "UserId");
            return View();
        }

        // POST: Panier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PanierId,UserId,CreationDate")] Panier panier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Consomateurs, "UserId", "UserId", panier.UserId);
            return View(panier);
        }

        // GET: Panier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Paniers.FindAsync(id);
            if (panier == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Consomateurs, "UserId", "UserId", panier.UserId);
            return View(panier);
        }

        // POST: Panier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PanierId,UserId,CreationDate")] Panier panier)
        {
            if (id != panier.PanierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierExists(panier.PanierId))
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
            ViewData["UserId"] = new SelectList(_context.Consomateurs, "UserId", "UserId", panier.UserId);
            return View(panier);
        }

        // GET: Panier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panier = await _context.Paniers
                .Include(p => p.Consomateur)
                .FirstOrDefaultAsync(m => m.PanierId == id);
            if (panier == null)
            {
                return NotFound();
            }

            return View(panier);
        }

        // POST: Panier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panier = await _context.Paniers.FindAsync(id);
            if (panier != null)
            {
                _context.Paniers.Remove(panier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierExists(int id)
        {
            return _context.Paniers.Any(e => e.PanierId == id);
        }
    }
}

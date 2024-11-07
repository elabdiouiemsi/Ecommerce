using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class PanierItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PanierItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PanierItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PanierItems.Include(p => p.Panier).Include(p => p.Produit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PanierItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panierItem = await _context.PanierItems
                .Include(p => p.Panier)
                .Include(p => p.Produit)
                .FirstOrDefaultAsync(m => m.PanierItemId == id);
            if (panierItem == null)
            {
                return NotFound();
            }

            return View(panierItem);
        }

        // GET: PanierItem/Create
        public IActionResult Create()
        {
            ViewData["PanierId"] = new SelectList(_context.Paniers, "PanierId", "PanierId");
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId");
            return View();
        }

        // POST: PanierItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PanierItemId,PanierId,ProduitId,Quantite,Prix")] PanierItem panierItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(panierItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PanierId"] = new SelectList(_context.Paniers, "PanierId", "PanierId", panierItem.PanierId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId", panierItem.ProduitId);
            return View(panierItem);
        }

        // GET: PanierItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panierItem = await _context.PanierItems.FindAsync(id);
            if (panierItem == null)
            {
                return NotFound();
            }
            ViewData["PanierId"] = new SelectList(_context.Paniers, "PanierId", "PanierId", panierItem.PanierId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId", panierItem.ProduitId);
            return View(panierItem);
        }

        // POST: PanierItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PanierItemId,PanierId,ProduitId,Quantite,Prix")] PanierItem panierItem)
        {
            if (id != panierItem.PanierItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(panierItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanierItemExists(panierItem.PanierItemId))
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
            ViewData["PanierId"] = new SelectList(_context.Paniers, "PanierId", "PanierId", panierItem.PanierId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId", panierItem.ProduitId);
            return View(panierItem);
        }

        // GET: PanierItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var panierItem = await _context.PanierItems
                .Include(p => p.Panier)
                .Include(p => p.Produit)
                .FirstOrDefaultAsync(m => m.PanierItemId == id);
            if (panierItem == null)
            {
                return NotFound();
            }

            return View(panierItem);
        }

        // POST: PanierItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var panierItem = await _context.PanierItems.FindAsync(id);
            if (panierItem != null)
            {
                _context.PanierItems.Remove(panierItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PanierItemExists(int id)
        {
            return _context.PanierItems.Any(e => e.PanierItemId == id);
        }
    }
}

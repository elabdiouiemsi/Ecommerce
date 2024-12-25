using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produits.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProduitId == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        public IActionResult Create()
{
    // Vous passez la liste des catégories à la vue via ViewBag
    ViewBag.category = _context.Categories.ToList();
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("ProduitId,Nom,Prix,Description,ImageUrl,Quantite,CategoryId")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                // Ajouter le produit à la base de données
                _context.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recharger les catégories si la validation échoue
            ViewBag.category = _context.Categories.ToList();
            return View(produit);
        }
        public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var produit = await _context.Produits.FindAsync(id);
    if (produit == null)
    {
        return NotFound();
    }
    ViewBag.category = _context.Categories.ToList();  // Récupération des catégories pour le dropdown
    return View(produit);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("ProduitId,Nom,Prix,Description,ImageUrl,Quantite,CategoryId")] Produit produit)
{
    if (id != produit.ProduitId)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
                _context.Update(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                
    }
    ViewBag.category = _context.Categories.ToList();  // Récupération des catégories pour le dropdown
    return View(produit);
}

        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produits
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProduitId == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Méthode privée pour éviter la répétition du code dans Create et Edit
        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from c in _context.Categories
                                  orderby c.Nom
                                  select c;
            ViewData["CategoryId"] = new SelectList(categoriesQuery.AsNoTracking(), "CategoryId", "Nom", selectedCategory);
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.ProduitId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers;
    using ecommerce.Data;

    public class CommandeItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommandeItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CommandeItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CommandeItems.Include(c => c.Commande).Include(c => c.Produit);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CommandeItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeItem = await _context.CommandeItems
                .Include(c => c.Commande)
                .Include(c => c.Produit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commandeItem == null)
            {
                return NotFound();
            }

            return View(commandeItem);
        }

        // GET: CommandeItems/Create
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId");
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId");
            return View();
        }

        // POST: CommandeItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CommandeId,ProduitId,Quantite,Prix")] CommandeItem commandeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commandeItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", commandeItem.CommandeId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId", commandeItem.ProduitId);
            return View(commandeItem);
        }

        // GET: CommandeItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeItem = await _context.CommandeItems.FindAsync(id);
            if (commandeItem == null)
            {
                return NotFound();
            }
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", commandeItem.CommandeId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId", commandeItem.ProduitId);
            return View(commandeItem);
        }

        // POST: CommandeItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CommandeId,ProduitId,Quantite,Prix")] CommandeItem commandeItem)
        {
            if (id != commandeItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commandeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeItemExists(commandeItem.Id))
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
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", commandeItem.CommandeId);
            ViewData["ProduitId"] = new SelectList(_context.Produits, "ProduitId", "ProduitId", commandeItem.ProduitId);
            return View(commandeItem);
        }

        // GET: CommandeItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commandeItem = await _context.CommandeItems
                .Include(c => c.Commande)
                .Include(c => c.Produit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commandeItem == null)
            {
                return NotFound();
            }

            return View(commandeItem);
        }

        // POST: CommandeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commandeItem = await _context.CommandeItems.FindAsync(id);
            if (commandeItem != null)
            {
                _context.CommandeItems.Remove(commandeItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeItemExists(int id)
        {
            return _context.CommandeItems.Any(e => e.Id == id);
        }
    }


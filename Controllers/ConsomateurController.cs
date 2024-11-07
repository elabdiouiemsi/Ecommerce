using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class ConsomateurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsomateurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consomateur
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consomateurs.ToListAsync());
        }

        // GET: Consomateur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consomateur = await _context.Consomateurs
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (consomateur == null)
            {
                return NotFound();
            }

            return View(consomateur);
        }

        // GET: Consomateur/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consomateur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Nom,Prenom,Email,Password,Role")] Consomateur consomateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consomateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consomateur);
        }

        // GET: Consomateur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consomateur = await _context.Consomateurs.FindAsync(id);
            if (consomateur == null)
            {
                return NotFound();
            }
            return View(consomateur);
        }

        // POST: Consomateur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Nom,Prenom,Email,Password,Role")] Consomateur consomateur)
        {
            if (id != consomateur.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consomateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsomateurExists(consomateur.UserId))
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
            return View(consomateur);
        }

        // GET: Consomateur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consomateur = await _context.Consomateurs
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (consomateur == null)
            {
                return NotFound();
            }

            return View(consomateur);
        }

        // POST: Consomateur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consomateur = await _context.Consomateurs.FindAsync(id);
            if (consomateur != null)
            {
                _context.Consomateurs.Remove(consomateur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsomateurExists(int id)
        {
            return _context.Consomateurs.Any(e => e.UserId == id);
        }
    }
}

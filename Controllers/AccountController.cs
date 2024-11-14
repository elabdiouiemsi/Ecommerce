using Microsoft.AspNetCore.Mvc;
using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ecommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action pour afficher le formulaire de connexion
        public IActionResult Login()
        {
            return View();
        }

        // Action pour traiter la connexion
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Chercher l'utilisateur dans la base de données, que ce soit Admin ou Consommateur
                var admin = _context.Admins
                    .FirstOrDefault(a => a.Email == model.Email && a.Password == model.Password);
                
                var consommateur = _context.Consomateurs
                    .FirstOrDefault(c => c.Email == model.Email && c.Password == model.Password);

                if (admin != null)
                {
                    // Rediriger vers le tableau de bord de l'Admin
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (consommateur != null)
                {
                    // Rediriger vers le tableau de bord du Consommateur
                    return RedirectToAction("ConsommateurDashboard", "Consommateur");
                }
                else
                {
                    ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe incorrect.");
                }
            }

            return View(model);
        }
    }
}

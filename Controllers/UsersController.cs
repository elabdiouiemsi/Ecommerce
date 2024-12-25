using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Liste des utilisateurs
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();  // Assurez-vous d'importer "Microsoft.EntityFrameworkCore"
        return View(users);
    }

    // Ajouter un utilisateur (GET)
    public IActionResult Create()
    {
        return View();
    }

    // Ajouter un utilisateur (POST)
    [HttpPost]
    public async Task<IActionResult> Create(string email, string password, string role = "Consomateur")
    {
        if (ModelState.IsValid)
        {
            // Vérifier si un utilisateur avec cet email existe déjà
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Un utilisateur avec cet email existe déjà.");
                return View();
            }

            // Créer un nouvel utilisateur
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Vérifier si le rôle existe, sinon le créer
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }

                // Ajouter l'utilisateur au rôle spécifié
                await _userManager.AddToRoleAsync(user, role);
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View();
    }

    // Modifier un utilisateur (GET)
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // Modifier un utilisateur (POST)
    [HttpPost]
    public async Task<IActionResult> Edit(string id, string email)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            user.Email = email;
            user.UserName = email;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(user);
    }

    // Supprimer un utilisateur
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return RedirectToAction(nameof(Index));
    }
}

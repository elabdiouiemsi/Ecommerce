using ecommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Ajouter le contexte de la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les services Identity avec gestion des rôles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true) // Confirmation d'email activée
    .AddRoles<IdentityRole>() // Gestion des rôles
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Ajouter les contrôleurs et Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Vérifiez si des catégories existent déjà, sinon les ajouter
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Nom = "Électronique" },
            new Category { Nom = "Mode" },
            new Category { Nom = "Maison et Jardin" },
            new Category { Nom = "Sport" },
            new Category { Nom = "Santé et Beauté" }
        );

        // Sauvegarder les changements dans la base de données
        context.SaveChanges();
    }
}


// Ajouter les rôles et un utilisateur Admin au démarrage
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // Définir les rôles à créer
    string[] roleNames = { "Admin", "Consomateur" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Créer un utilisateur Admin par défaut
    var adminEmail = "admin@example.com";
    var adminPassword = "Admin@123"; // Utilisez un mot de passe fort
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true // Simuler la confirmation d'email
        };
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// Configurer le pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Activer l'authentification et l'autorisation
app.UseAuthentication(); // Nécessaire pour Identity
app.UseAuthorization();

// Configurer les routes
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();

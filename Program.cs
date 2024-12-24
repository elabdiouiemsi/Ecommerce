using ecommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Ajouter le contexte de la base de donn�es
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les services Identity avec gestion des r�les
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true) // Confirmation d'email activ�e
    .AddRoles<IdentityRole>() // Gestion des r�les
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Ajouter les contr�leurs et Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // V�rifiez si des cat�gories existent d�j�, sinon les ajouter
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Nom = "�lectronique" },
            new Category { Nom = "Mode" },
            new Category { Nom = "Maison et Jardin" },
            new Category { Nom = "Sport" },
            new Category { Nom = "Sant� et Beaut�" }
        );

        // Sauvegarder les changements dans la base de donn�es
        context.SaveChanges();
    }
}


// Ajouter les r�les et un utilisateur Admin au d�marrage
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    // D�finir les r�les � cr�er
    string[] roleNames = { "Admin", "Consomateur" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Cr�er un utilisateur Admin par d�faut
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
app.UseAuthentication(); // N�cessaire pour Identity
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

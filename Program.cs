using ecommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


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
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // D�finir les r�les � cr�er
        string[] roleNames = { "Admin", "Consomateur" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    Console.WriteLine($"Erreur lors de la cr�ation du r�le {roleName}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }

        // Cr�er un utilisateur Admin par d�faut
        var adminEmail = "admin@example.com";
        var adminPassword = "Admin@123"; // Remplacer par une variable d'environnement
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true // Simuler la confirmation d'email
            };
            var userResult = await userManager.CreateAsync(adminUser, adminPassword);
            if (userResult.Succeeded)
            {
                var roleAssignResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!roleAssignResult.Succeeded)
                {
                    Console.WriteLine($"Erreur lors de l'ajout de l'utilisateur Admin au r�le : {string.Join(", ", roleAssignResult.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                Console.WriteLine($"Erreur lors de la cr�ation de l'utilisateur Admin : {string.Join(", ", userResult.Errors.Select(e => e.Description))}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'initialisation des r�les et utilisateurs : {ex.Message}");
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

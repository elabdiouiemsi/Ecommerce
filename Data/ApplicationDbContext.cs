using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ecommerce.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Commande> Commandes { get; set; }
    public DbSet<CommandeItem> CommandeItems { get; set; }
    public DbSet<Consomateur> Consomateurs { get; set; }
    public DbSet<Panier> Paniers { get; set; }
    public DbSet<PanierItem> PanierItems { get; set; }
    public DbSet<Produit> Produits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Définissez UserId comme clé primaire pour Consomateur
        modelBuilder.Entity<Consomateur>()
            .HasKey(c => c.UserId);

        // Configurez les autres relations
        modelBuilder.Entity<Commande>()
            .HasMany(c => c.CommandeItems)
            .WithOne(ci => ci.Commande)
            .HasForeignKey(ci => ci.CommandeId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Panier>()
            .HasMany(p => p.PanierItems)
            .WithOne(pi => pi.Panier)
            .HasForeignKey(pi => pi.PanierId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommandeItem>()
            .HasOne(ci => ci.Produit)
            .WithMany(p => p.CommandeItems) 
            .HasForeignKey(ci => ci.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PanierItem>()
            .HasOne(pi => pi.Produit)
            .WithMany(p => p.PanierItems)
            .HasForeignKey(pi => pi.ProduitId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Consomateur>()
            .HasOne(c => c.Panier)
            .WithOne(p => p.Consomateur)
            .HasForeignKey<Panier>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Consomateur>()
            .HasMany(c => c.Commandes)
            .WithOne(co => co.Consomateur)
            .HasForeignKey(co => co.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Commande>()
            .Property(c => c.TotalPayer)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<CommandeItem>()
            .Property(ci => ci.Prix)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<PanierItem>()
            .Property(pi => pi.Prix)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Produit>()
            .Property(p => p.Prix)
            .HasColumnType("decimal(18, 2)");

    }
}

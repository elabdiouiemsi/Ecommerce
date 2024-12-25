using System.ComponentModel.DataAnnotations;

public class Produit
{
    public int ProduitId { get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à zéro.")]
    public decimal Prix { get; set; }

    public string Description { get; set; }

    
    public int ? CategoryId { get; set; }
    public Category ? Category { get; set; }

    public string ImageUrl { get; set; }

   
    public int Quantite { get; set; }

   
    public ICollection<CommandeItem> ? CommandeItems { get; set; }

   
    public ICollection<PanierItem> ? PanierItems { get; set; }
}

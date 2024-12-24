using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Produit : IValidatableObject
{
    public int ProduitId { get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à zéro.")]
    public decimal Prix { get; set; }

    public string Description { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public Category Category { get; set; }

    public string ImageUrl { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être au moins de 1.")]
    public int Quantite { get; set; }

    // Ajouter les propriétés manquantes
    public ICollection<CommandeItem> CommandeItems { get; set; }
    public ICollection<PanierItem> PanierItems { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CategoryId <= 0)
        {
            yield return new ValidationResult("La catégorie est invalide.", new[] { nameof(CategoryId) });
        }
    }
}

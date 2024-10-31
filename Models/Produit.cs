public class Produit
{
    public int ProduitId { get; set; }
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string ImageUrl { get; set; }
    public int Quantite { get; set; }
    public ICollection<CommandeItem> CommandeItems { get; set; }
    public ICollection<PanierItem> PanierItems { get; set; }
}
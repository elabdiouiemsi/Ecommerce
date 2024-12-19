public class Category
{
    public int CategoryId { get; set; }
    public string Nom { get; set; }
    public List<Produit> Produits { get; set; } = new List<Produit>();
}
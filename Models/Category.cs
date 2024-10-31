public class Category
{
    public int CategoryId { get; set; }
    public string Nom { get; set; }
    public ICollection<Produit> Produits { get; set; }
}
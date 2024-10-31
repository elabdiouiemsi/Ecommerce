public class PanierItem
{
    public int PanierItemId { get; set; }
    public int PanierId { get; set; }
    public int ProduitId { get; set; }
    public int Quantite { get; set; }
    public decimal Prix { get; set; }

    public Produit Produit { get; set; }
    public Panier Panier { get; set; }
}
public class Commande
{
    public int CommandeId { get; set; }
    public int UserId { get; set; }
    public DateTime DateCommande { get; set; }
    public decimal TotalPayer { get; set; }
    public string CommandeStatus { get; set; }
    public List<CommandeItem> CommandeItems { get; set; }
    public Consomateur Consomateur { get; set; }
}
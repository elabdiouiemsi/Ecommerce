using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Consomateur
{
    [Key] // Spécifie explicitement que UserId est la clé primaire
    public int UserId { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public Panier Panier { get; set; }
    public ICollection<Commande> Commandes { get; set; }
}

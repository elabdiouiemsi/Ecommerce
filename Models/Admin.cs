using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
public class Admin
{
    [Key] // Assurez-vous d'ajouter une annotation [Key] si ce n'est pas une clé primaire implicite
    public int AdminId { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Role { get; set; }  // Ajout de la propriété Role
}

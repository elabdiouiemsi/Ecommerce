using Microsoft.AspNetCore.Mvc;

public class CategoriesController : Controller
{

    // Création d'une liste de catégories avec des données exemple
    List<Category> CatList = new List<Category>
{
    new Category
    {
        CategoryId = 1,
        Nom = "Électronique"
    },
    new Category
    {
        CategoryId = 2,
        Nom = "Mode"
    },
    new Category
    {
        CategoryId = 3,
        Nom = "Maison et Jardin"
    },
    new Category
    {
        CategoryId = 4,
        Nom = "Sport"
    },
    new Category
    {
        CategoryId = 5,
        Nom = "Santé et Beauté"
    }

};
   

    public IActionResult Index()
    {
        ViewBag.category=CatList;
        return View();
    }
    

    
}

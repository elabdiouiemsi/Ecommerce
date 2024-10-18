using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers;

public class TaskController : Controller {
    private readonly TodoDbContext _context;

    public TaskController(TodoDbContext context){
        _context = context;
    }

    public IActionResult Index(){
        var task = _context.Tasks.ToList();
        return View();
    }
      //modifier une tache existant
    public IActionResult create(){
        return View();
    }
  
    [HttpPost]
    public IActionResult create(Task task){
        if(ModelState.IsValid){
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(task);
    }
[HttpPost]
    public IActionResult edit (int id){
        var task = _context.Tasks.Find(id);
        if (task == null)
        {
            return NotFound();
        }
        return View(task);
    }

    public IActionResult edit(Task task){
        if (ModelState.IsValid)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        return View(task);
    }

}
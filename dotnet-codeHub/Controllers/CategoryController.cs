using dotnet_codeHub.Data;
using dotnet_codeHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_codeHub.Controllers
{
    public class CategoryController : Controller
    {
        protected readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.OrderBy(c => c.DisplayOrder).ToList();
            return View(categoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
			if (String.IsNullOrWhiteSpace(category.Name))
			{
				ModelState.AddModelError("Name", "Il nome è obbligatorio");
                TempData["error"] = "Correggi i campi richiesti";
            }
			else if (_db.Categories.Any(n => n.Name.ToLower().Replace(" ", "") == category.Name.ToLower().Replace(" ", "")))
			{
				ModelState.AddModelError("Name", "Categoria già esistente");
                TempData["error"] = "Correggi i campi richiesti";
            }

			if (ModelState.IsValid)
            {
				_db.Categories.Add(category);
				_db.SaveChanges();
                TempData["success"] = "Categoria aggiunta!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Correggi i campi richiesti";
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            if((id == null) && (id == 0)){
                return NotFound();
            }
            Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
			{
				_db.Categories.Update(category);
				_db.SaveChanges();
                TempData["success"] = "Categoria modificata!";
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = _db.Categories.FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Categoria eliminata!";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            var records = _db.Categories.ToList();
            _db.Categories.RemoveRange(records);
            _db.SaveChanges();
            TempData["success"] = "Categorie eliminate";
            return RedirectToAction("Index");
        }
    }
}

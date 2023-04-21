using dotnet_codeHub.Data;
using dotnet_codeHub.Models;
using Microsoft.AspNetCore.Mvc;

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
            List<Category> categoryList = _db.Categories.OrderBy(c => c.Id).ToList();
            return View(categoryList);
        }
    }
}

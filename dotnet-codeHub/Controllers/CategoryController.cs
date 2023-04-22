using codeHub.DataAccess.Data;
using codeHub.DataAccess.Repository.IRepository;
using codeHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace codeHub.DataAccess.Controllers
{
    public class CategoryController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();
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
			else if (_unitOfWork.Category.Get(n => n.Name.ToLower().Replace(" ", "") == category.Name.ToLower().Replace(" ", "")) != null)
			{
				ModelState.AddModelError("Name", "Categoria già esistente");
                TempData["error"] = "Correggi i campi richiesti";
            }

			if (ModelState.IsValid)
            {
				_unitOfWork.Category.Add(category);
				_unitOfWork.Save();
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
            Category category = _unitOfWork.Category.Get(c => c.Id == id);
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
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
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
            Category category = _unitOfWork.Category.Get(c => c.Id == id);
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
            Category category = _unitOfWork.Category.Get(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Categoria eliminata!";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteAll()
        {
            var records = _unitOfWork.Category.GetAll();
            _unitOfWork.Category.RemoveRange(records);
            _unitOfWork.Save();
            TempData["success"] = "Categorie eliminate";
            return RedirectToAction("Index");
        }
    }
}

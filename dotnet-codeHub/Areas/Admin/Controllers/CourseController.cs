using codeHub.DataAccess.Repository.IRepository;
using codeHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dotnet_codeHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Course> courseList = _unitOfWork.Course.GetAll().ToList();

            return View(courseList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            ViewBag.CategoryList = categoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course course)
        {
            if(string.IsNullOrWhiteSpace(course.Title))
            {
                ModelState.AddModelError("Title","Il titolo è obbligatorio");
                TempData["error"] = "Correggi i campi richiesti";
            }
            else if (_unitOfWork.Course.Get(n => n.Title.ToLower().Replace(" ", "") == course.Title.ToLower().Replace(" ", "")) != null)
            {
                ModelState.AddModelError("Name", "Corso già esistente");
                TempData["error"] = "Correggi i campi richiesti";
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Course.Add(course);
                _unitOfWork.Save();
                TempData["success"] = "Corso creato correttamente";
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
            if (id == null && id == 0)
            {
                return NotFound();
            }
            Course course = _unitOfWork.Course.Get(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (course == null)
            {
                return NotFound();  
            }
            _unitOfWork.Course.Update(course);
            _unitOfWork.Save();
            TempData["success"] = "Corso modificato con successo";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Course course = _unitOfWork.Course.Get(c => c.Id == id);
            return View(course);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCourse(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Course course = _unitOfWork.Course.Get(c => c.Id == id);
            _unitOfWork.Course.Remove(course);
            _unitOfWork.Save();
            TempData["success"] = "Corso eliminato con successo";
            return RedirectToAction("Index"); 
        }
    }
}

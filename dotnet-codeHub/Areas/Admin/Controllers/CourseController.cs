using codeHub.DataAccess.Repository.IRepository;
using codeHub.Models;
using codeHub.Models.ViewModels;
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
            CourseVM courseVM = new()
            {
                CategoryList = categoryList,
                Course = new Course(),
            };
            return View(courseVM);
        }
        [HttpPost]
        public IActionResult Create(CourseVM courseVM)
        {
            if(string.IsNullOrWhiteSpace(courseVM.Course.Title))
            {
                ModelState.AddModelError("Title","Il titolo è obbligatorio");
                TempData["error"] = "Correggi i campi richiesti";
            }
            else if (_unitOfWork.Course.Get(n => n.Title.ToLower().Replace(" ", "") == courseVM.Course.Title.ToLower().Replace(" ", "")) != null)
            {
                ModelState.AddModelError("Name", "Corso già esistente");
                TempData["error"] = "Correggi i campi richiesti";
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Course.Add(courseVM.Course);
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
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            });
            CourseVM courseVM = new()
            {
                Course = course,
                CategoryList = categoryList

            };

            return View(courseVM);
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
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
               Text= c.Name,
               Value = c.Id.ToString(),
            });
            CourseVM courseVM = new()
            {
                Course = course,
                CategoryList = categoryList
            };
            return View(courseVM);
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

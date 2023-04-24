using codeHub.DataAccess.Repository.IRepository;
using codeHub.Models;
using codeHub.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace dotnet_codeHub.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CourseController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<Course> courseList = _unitOfWork.Course.GetAll().ToList();

            return View(courseList);
        }
        public IActionResult UpSert(int? id)
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
            if(id == null || id == 0)
            {
                return View(courseVM);
            }
            else
            {
                //update
                courseVM.Course = _unitOfWork.Course.Get(c => c.Id == id);
                return View(courseVM);
            }
        }

        [HttpPost]
        public IActionResult UpSert(CourseVM courseVM, IFormFile? file)
        {
            if (string.IsNullOrWhiteSpace(courseVM.Course.Title))
            {
                ModelState.AddModelError("Title", "Il titolo è obbligatorio");
                TempData["error"] = "Correggi i campi richiesti";
            }
            else if (courseVM.Course.Id == 0 && _unitOfWork.Course.Get(n => n.Title.ToLower().Replace(" ", "") == courseVM.Course.Title.ToLower().Replace(" ", "")) != null)
            {
                ModelState.AddModelError("Title", "Corso già esistente");
                TempData["error"] = "Correggi i campi richiesti";
            }
            if (ModelState.IsValid)
            {

                string pathWWW = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string coursePath = Path.Combine(pathWWW, @"images\course");
                    string filePath = Path.Combine(coursePath, fileName);

                    // cancella la vecchia immagine, se esiste
                    string oldImagePath = "";
                    if (courseVM.Course != null)
                    {
                        oldImagePath = courseVM.Course.Image?.TrimStart('\\');
                    }
                    if (!string.IsNullOrEmpty(oldImagePath))
                    {
                        string oldFilePath = Path.Combine(pathWWW, oldImagePath);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // verifica se la directory esiste e creala se necessario
                    if (!Directory.Exists(coursePath))
                    {
                        Directory.CreateDirectory(coursePath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    courseVM.Course.Image = @"\images\course\" + fileName;
                }

                if (courseVM.Course.Id == 0)
                {
                    _unitOfWork.Course.Add(courseVM.Course);
                    TempData["success"] = "Corso creato correttamente";
                }
                else
                {
                    _unitOfWork.Course.Update(courseVM.Course);
                    TempData["success"] = "Corso modificato correttamente";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                courseVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(courseVM);
            }
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

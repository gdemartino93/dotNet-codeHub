using codeHub.DataAccess.Repository.IRepository;
using codeHub.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotnet_codeHub.Areas.Users.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Course> courseList = _unitOfWork.Course.GetAllCoursesWithCategories();
            return View(courseList);
        }

        public IActionResult Details(int id)
        {
            Course course = _unitOfWork.Course.GetAllCoursesWithCategories().FirstOrDefault(c => c.Id == id);
            return View(course);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
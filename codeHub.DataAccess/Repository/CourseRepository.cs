using codeHub.DataAccess.Data;
using codeHub.DataAccess.Repository.IRepository;
using codeHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeHub.DataAccess.Repository
{
    internal class CourseRepository : Repository<Course> , ICourseRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Course> GetAllCoursesWithCategories()
        {
            return _db.Courses.Include(c => c.Category).ToList();
        }

        public void Update(Course course)
        {
            _db.Update(course);
        }
    }
}

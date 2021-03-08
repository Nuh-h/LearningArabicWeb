using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArabicLearning.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArabicLearning.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesRepository coursesRepo;
        public CoursesController(ICoursesRepository Courses)
        {
            this.coursesRepo = Courses;
        }
        public IActionResult Index()
        {
            return View(coursesRepo.GetAllCourses());
        }
        
        [HttpGet("Courses/New")]
        public IActionResult NewCourses()
        {
            return View(coursesRepo.GetAllNew());
        }
        [HttpGet("Courses/Popular")]
        public IActionResult PopularCourses()
        {
            return View(coursesRepo.GetAllPopular());
        }
        [HttpGet("Courses/{id}/Detail")]
        public IActionResult CourseDetails(int id)
        {
            return View(coursesRepo.GetFullCourse(id));
        }

    }
}

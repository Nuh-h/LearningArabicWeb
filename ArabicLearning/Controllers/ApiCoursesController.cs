using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArabicLearning.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ArabicLearning.Controllers
{
    //[Route("/api/*")]
    public class ApiCourses : Controller
    {
        private readonly ICoursesRepository coursesRepo;
        public ApiCourses(ICoursesRepository coursesRepository)
        {
            this.coursesRepo = coursesRepository;
        }
        [HttpGet("api/courses/all")]
        [HttpGet("api/courses")]
        public IActionResult GetAll()
        {
            return new OkObjectResult(coursesRepo.GetAllCourses());
        }

        [HttpGet("api/courses/grammar")]
        public IActionResult GetGrammar()
        {
            return new OkObjectResult(coursesRepo.GetAllGrammar());
        }

        [HttpGet("api/courses/morphology")]
        public IActionResult GetMorphology()
        {
            return new OkObjectResult(coursesRepo.GetAllMorphology());
        }

        [HttpGet("api/courses/{level}")]
        public IActionResult GetCoursesOfLevel(string level)
        {
            return new OkObjectResult(coursesRepo.GetLevel(level));
        }
    }
}

using ArabicLearning.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabicLearning.Repositories.Interfaces
{
    public interface ICoursesRepository
    {
        IEnumerable<Course> GetAllCourses();
        IEnumerable<Course> GetAllGrammar();
        IEnumerable<Course> GetAllMorphology();
        IEnumerable<Course> GetLevel(string level);
        IEnumerable<Course> GetAllPopular();
        IEnumerable<Course> GetAllNew();
        IEnumerable<Course> GetFullCourse(int id);




    }
}

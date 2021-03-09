using ArabicLearning.Repositories.Interfaces;
using ArabicLearning.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ArabicLearning.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {   
        private String queryStr;
        private SqlConnection myConn;
        private IEnumerable<Course> Collection;
        public CoursesRepository()
        {
            this.Collection = InMemoryCourseCollection;
            
            myConn = new SqlConnection("Server=LAPTOP-WELHVMIL;Database=LearningArabicDB;Trusted_Connection=True;");

            //used this command to create database
            //queryStr = "CREATE DATABASE LearningArabicDB;";

            //now, will use this command to create the table for courses
            //queryStr = "CREATE TABLE Courses( course_id INT NOT NULL IDENTITY PRIMARY KEY, course_name VARCHAR(30), ... )";

            //we will now use this command to add in the initial values
/*            queryStr = "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 2', 'Elementary', 'Aq', 'Syntax');" +
                "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 3', 'Lower Intermediate', 'Aq', 'Morphology');" +
                "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 4', 'Upper Intermediate', 'Aq', 'Rhetoric');" +
                "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 5', 'Advanced', 'Aq', 'Tarqeeq')";*/

            /* queryStr = "";
             using (SqlCommand myCommand = new SqlCommand(queryStr, myConn))
             {
                 try
                 {
                     myConn.Open();
                     myCommand.ExecuteNonQuery();
                     System.Diagnostics.Debug.WriteLine("DataBase connection is ok");
                 }
                 catch (System.Exception ex)
                 {
                     System.Diagnostics.Debug.WriteLine(ex.ToString());
                 }
                 finally
                 {
                     if (myConn.State == ConnectionState.Open)
                     {
                         myConn.Close();
                     }
                 }
             }*/
        }

        public IEnumerable<Course> GetAllCourses()
        {
            queryStr = "SELECT * From Courses";
            //return this.Collection;
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetAllGrammar()
        {
            queryStr = "SELECT * From Courses WHERE course_type='Grammar';";
            //return this.Collection.GroupBy(course => course.Type=="Nahw").FirstOrDefault();
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetAllMorphology()
        {
            queryStr = "SELECT * From Courses WHERE course_type='Morphology';";
            //return this.Collection.Where(course => course.Type=="Sarf");
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetLevel(string level)
        {
            queryStr = string.Format("SELECT * From Courses WHERE course_level='{0}'",level);
            //return this.Collection.GroupBy(course => course.Level == level).FirstOrDefault();
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetAllNew()
        {
            queryStr = "SELECT * From Courses ORDER BY course_createdon DESC;";
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetAllPopular()
        {
            queryStr = "SELECT * From Courses ORDER BY course_popularity DESC;";
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetFullCourse(int id)
        {
            queryStr = string.Format("SELECT * From Courses WHERE course_id={0};",id);
            return GetResultFromDB(queryStr);
        }
        private IEnumerable<Course> GetResultFromDB(string queryStr)
        {
            List<Course> resultList = new List<Course>();
            using (SqlCommand myCommand = new SqlCommand(queryStr, myConn))
            {
                try
                {
                    myConn.Open();
                    SqlDataReader myData = myCommand.ExecuteReader();
                    if (myData.HasRows)
                    {
                        while (myData.Read())
                        {
                            Course course = new Course();
                            course.Id = (int) myData["course_id"];
                            course.Name = (string) myData["course_name"];
                            course.Level = (string)myData["course_level"];
                            course.Teacher = (string)myData["course_teacher"];
                            course.Type = (string)myData["course_type"];

                            resultList.Add(course);
                            System.Diagnostics.Debug.WriteLine("-- Database query successful --");
                            //System.Diagnostics.Debug.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", myData.GetInt32(0), myData.GetString(1), myData.GetString(2), myData.GetString(3), myData.GetString(4));
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No rows found.");
                    }
                    myData.Close();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }
            }
            return resultList;
        }
        private IEnumerable<Course> InMemoryCourseCollection { get; } = new List<Course>
        {
            new Course { Name = "Classical Arabic 1", Level = "Beginner", Teacher = "Aq", Type = "Nahw" },
            new Course { Name = "Classical Arabic 2", Level = "Elementary", Teacher = "Aq", Type = "Sarf" },
            new Course { Name = "Classical Arabic 3", Level = "Lower Intermediate", Teacher = "Aq", Type = "Lugha" },
            new Course { Name = "Classical Arabic 4", Level = "Upper Intermediate", Teacher = "Aq", Type = "Balagha" },
            new Course { Name = "Classical Arabic 5", Level = "Advanced", Teacher = "Aq", Type = "Tarqeeq" }
        };        
    }
}

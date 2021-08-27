using ArabicLearning.Repositories.Interfaces;
using ArabicLearning.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
//mysql
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ArabicLearning.Repositories
{
    public class CoursesRepository : ICoursesRepository
    {   
        private String queryStr;
        private MySqlConnection myConnection;
        //private IEnumerable<Course> Collection;
        public CoursesRepository()
        {
            //this.Collection = InMemoryCourseCollection;
            try{
                myConnection = new MySqlConnection();
                myConnection.ConnectionString = "Server=localhost;Database=LearningArabicDB;Uid=root;Port=3306;";
                myConnection.Open();
            }
            catch(MySqlException ex) {
                Console.WriteLine(ex.Message);
            }
            //-- for creating database
            //queryStr = "CREATE DATABASE LearningArabicDB;";

            //-- for creating the table for courses
            //queryStr = "CREATE TABLE Courses( course_id INT NOT NULL IDENTITY PRIMARY KEY, course_name VARCHAR(30), ... )";

            //-- to populate some initial items into the table
            /*queryStr = "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 2', 'Elementary', 'Aq', 'Syntax');" +
                "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 3', 'Lower Intermediate', 'Aq', 'Morphology');" +
                "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 4', 'Upper Intermediate', 'Aq', 'Rhetoric');" +
                "INSERT INTO Courses(course_name, course_level, course_teacher, course_type) VALUES('Classic Arabic 5', 'Advanced', 'Aq', 'Poetry')";*/

            /* queryStr = "";
             using (SqlCommand myCommand = new SqlCommand(queryStr, myConnection))
             {
                 try
                 {
                     myConnection.Open();
                     myCommand.ExecuteNonQuery();
                     System.Diagnostics.Debug.WriteLine("DataBase connection is ok");
                 }
                 catch (System.Exception ex)
                 {
                     System.Diagnostics.Debug.WriteLine(ex.ToString());
                 }
                 finally
                 {
                     if (myConnection.State == ConnectionState.Open)
                     {
                         myConnection.Close();
                     }
                 }
             }*/
        }

        public IEnumerable<Course> GetAllCourses()
        {
            queryStr = "SELECT * From Course";
            //return this.Collection;
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetType(string courseType)
        {
            queryStr = string.Format("SELECT * From Course WHERE type='{0}'",courseType);
            //return this.Collection.GroupBy(course => course.Type==courseType).FirstOrDefault();
            return GetResultFromDB(queryStr);
        }

        public IEnumerable<Course> GetLevel(string level)
        {
            queryStr = string.Format("SELECT * From Course WHERE level='{0}'",level);
            //return this.Collection.GroupBy(course => course.Level == level).FirstOrDefault();
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetAllNew()
        {
            queryStr = "SELECT * From Course ORDER BY createdon DESC;";
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetAllPopular()
        {
            queryStr = "SELECT * From Course ORDER BY popularity DESC;";
            return GetResultFromDB(queryStr);
        }
        public IEnumerable<Course> GetFullCourse(int id)
        {
            queryStr = string.Format("SELECT * From Course WHERE course_id={0}",id);
            return GetResultFromDB(queryStr);
        }
        private IEnumerable<Course> GetResultFromDB(string queryStr)
        {
            List<Course> resultList = new List<Course>();
            using (MySqlCommand myCommand = new MySqlCommand(queryStr, myConnection))
            {
                try
                {
                    // myConnection.Open();
                    MySqlDataReader myData = myCommand.ExecuteReader();
                    if (myData.HasRows)
                    {
                        while (myData.Read())
                        {
                            Course course = new Course();
                            course.Id = (int) myData["course_id"];
                            course.Name = (string) myData["name"];
                            course.Level = (string)myData["level"];
                            // course.Teacher = (string)myData["teacher"];
                            course.Type = (string)myData["type"];

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
                    if (myConnection.State == ConnectionState.Open)
                    {
                        myConnection.Close();
                    }
                }
            }
            return resultList;
        }
        private IEnumerable<Course> InMemoryCourseCollection { get; } = new List<Course>
        {
            new Course { Name = "Classical Arabic 1", Level = "Beginner", Teacher = "Aq", Type = "Grammar" },
            new Course { Name = "Classical Arabic 2", Level = "Elementary", Teacher = "Aq", Type = "Morphology" },
            new Course { Name = "Classical Arabic 3", Level = "Lower Intermediate", Teacher = "Aq", Type = "Language" },
            new Course { Name = "Classical Arabic 4", Level = "Upper Intermediate", Teacher = "Aq", Type = "Eloquence" },
            new Course { Name = "Classical Arabic 5", Level = "Advanced", Teacher = "Aq", Type = "Poetry" }
        };        
    }
}

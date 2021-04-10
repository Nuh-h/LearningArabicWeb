using ArabicLearning.Repositories.Interfaces;
using ArabicLearning.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace ArabicLearning.Repositories
{
    public class UsersRepository : IUsersRepository
    {   
        private String queryStr;
        private SqlConnection myConnection;
        public UsersRepository()
        {
            
            myConnection = new SqlConnection("Server=LAPTOP-WELHVMIL;Database=LearningArabicDB;Trusted_Connection=True;");

        }

        public IEnumerable<User> GetAllUsers()
        {
            queryStr = "SELECT * From students";
            return GetResultFromDB(queryStr);
        }
        public string AddToDb(User user, string user_status)
        {
            var queryStr = "INSERT INTO "+user_status +" (first_name, last_name, email,password_hash)"+
                            "VALUES('"+user.firstName+"','"+user.lastName+"','"+user.Email+"','"+user.Hashed_Password+"')";
            var val = AddUserToDB(queryStr).Result;
            if(val!=0) return "success";
            
            return "fail";
        }
        private async Task<int?> AddUserToDB(string queryStr)
        {
            int? rowsAffected = null;
            using (SqlCommand myCommand = new SqlCommand(queryStr, myConnection))
            {
                try
                {
                    myConnection.Open();
                    rowsAffected = await myCommand.ExecuteNonQueryAsync();

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
            return rowsAffected;
        }

        private IEnumerable<User> GetResultFromDB(string queryStr)
        {
            List<User> resultList = new List<User>();
            using (SqlCommand myCommand = new SqlCommand(queryStr, myConnection))
            {
                try
                {
                    myConnection.Open();
                    SqlDataReader myData = myCommand.ExecuteReader();
                    if (myData.HasRows)
                    {
                        while (myData.Read())
                        {
                            User student = new User();
                            student.firstName = (string) myData["first_name"];
                            student.lastName = (string) myData["last_name"];
                            student.Email = (string) myData["email"];
                            student.Hashed_Password = (string)myData["password_hash"];

                            resultList.Add(student);
                            System.Diagnostics.Debug.WriteLine("-- Database query successful --");
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
    
    }
}

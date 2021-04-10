using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabicLearning.Repositories.Models
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAllUsers();
        //IEnumerable<User> GetResultFromDB(string queryStr);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArabicLearning.Repositories.Models;
namespace ArabicLearning.Repositories.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
        string Register(User user);
    }
}
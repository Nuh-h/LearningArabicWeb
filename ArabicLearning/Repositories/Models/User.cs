using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace ArabicLearning.Repositories.Models
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string Password { get; set; }
        public string Hashed_Password {get; set;}
        public string Salt { get; set; }

    }

    public static class Hash
    {
        public static string HashPassword(string text)//getHashSha256
        {   
            if(text == null) return "";
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}

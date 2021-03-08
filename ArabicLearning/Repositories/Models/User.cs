using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArabicLearning.Repositories.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        //
        public string Password { get; set; }
        //
        private string _password;
        public string Hashed_Password {
            get; set;
            //get { };
            //set{
            //    this._password = Password;
            //    this.Salt = this.makeSalt();
            //    this.Hashed_Password = this.EncryptPassword(password);
            //}
        }
        public string Salt { get; set; }
    }
}

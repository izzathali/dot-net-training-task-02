using ABC_Drive.DataModel;
using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.login
{
    class Login
    {
        public Login()
        {
            using (RentDbContext db = new RentDbContext())
            {
                var user = db.Users.Take(1).FirstOrDefault();
                if (user == null)
                {
                    List<DataModel.User> usr = new List<DataModel.User>
                    {
                        new User() { UserName = "admin", Password = "123"},
                    };
                    db.Users.AddRange(usr);
                    db.SaveChanges();
                }
            }
        }
        public bool CheckLogin(string Username , string Password)
        {
            bool Check = false;
            RentDbContext db = new RentDbContext();
            var Chk = db.Users.Any(u => u.UserName == Username && u.Password == Password);
            Check = Chk;

            return Check;
        }
    }
}

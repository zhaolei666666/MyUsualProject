using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EF;

namespace Domain.Business
{
    public class Login
    {
        public Users GetUsers(string accountName)
        {
            using (FirstEntities db = new FirstEntities())
            {
                Users user = db.Users.FirstOrDefault(u => u.AccountName == accountName);

                return user;
            }
        }

    }

}

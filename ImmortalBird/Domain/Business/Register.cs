﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.EF;

namespace Domain.Business
{
    public class Register
    {
        public void AddUser(Users user)
        {
            using (FirstEntities db = new FirstEntities())
            {
                try
                {
                    if (user != null)
                        db.Users.Add(user);

                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                }
            }
        }

    }
}

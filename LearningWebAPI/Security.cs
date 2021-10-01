using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentDataAccess;

namespace LearningWebAPI
{
    public class Security
    {
        public static bool Login(string username, string password)
        {
            using (DemoEntities entities = new DemoEntities())
            {
                return entities.Users.Any(user =>
                       user.username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.password == password);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercises.Models;

namespace Exercises
{
    class Startup
    {
        static void Main(string[] args)
        {
            var newUser = new Users.User()
            {
                Username = "Peter",
                Password = "Peter!",
                Email = "Peter@abv.bg",
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now,
                Age = 17,
                IsDeleted = false,
            };

            var newUser2 = new Users.User()
            {
                Username = "Ivan",
                Password = "Ivan@",
                Email = "Ivan.Ivanov@gmail.com",
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now,
                Age = 99,
                IsDeleted = false
            };

            var newUser3 = new Users.User()
            {
                Username = "Mariq",
                Password = "MariqMariqMariq",
                Email = "Mariq@abv.bg",
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now,
                Age = 27,
                IsDeleted = false
            };

            var context = new UsersContext();
            context.UsersTable.Add(newUser);
            context.UsersTable.Add(newUser2);
            context.UsersTable.Add(newUser3);

            context.SaveChanges();
        }
    }
}

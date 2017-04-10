using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class Startup
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var date = Convert.ToDateTime(input);
            var context = new UsersContext();
            var usersToDelete = context.UsersTable.Where(u => u.LastTimeLoggedIn < date);

            foreach (var user in usersToDelete)
            {
                user.IsDeleted = true;
            }

            Console.WriteLine(!usersToDelete.Any() ? "No users has been deleted" : $"{usersToDelete.Count()} users has been deleted");

            foreach (var user in usersToDelete)
            {
                context.UsersTable.Remove(user);
            }
        }
    }
}

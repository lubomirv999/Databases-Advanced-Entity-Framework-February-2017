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
            string input = Console.ReadLine();
            var context = new UsersContext();
            var users = context.UsersTable.Where(x => x.Email.EndsWith(input));

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username} {user.Email}");
            }
        }
    }
}

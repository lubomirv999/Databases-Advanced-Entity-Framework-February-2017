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
            //Task 1,2
            //Persons();

            //Task 3
            //Family();

            //Task 4
            //Student();

            //Task 5
            //Planck();

            //Task 6
            //Math();
        }

        private static void Planck()
        {
            Console.WriteLine(Calculation.GetPlanckConstant());
        }

        private static void Math()
        {
            //Task 6
            //Replace Sum with your action
            MathUtil math = new MathUtil();
            Console.WriteLine(MathUtil.Sum(1, 2));
        }

        private static void Student()
        {
            //Task 4
            string input = Console.ReadLine();

            while (input != "End")
            {
                Student st = new Student(input);
                input = Console.ReadLine();
            }

            Console.WriteLine(Exercises.Student.count);
        }

        private static void Family()
        {
            //Task 3
            int num = int.Parse(Console.ReadLine());
            Family fam = new Family();

            for (int i = 0; i < num; i++)
            {
                var args = Console.ReadLine().Split();
                Person currPerson = new Person(args[0], int.Parse(args[1]));
                fam.AddMember(currPerson);
            }

            Console.WriteLine(fam.GetOldestMember().Name + " " + fam.GetOldestMember().Age);

            //Family fam = new Family();
            //fam.AddMember(new Person("Pesho", 3));
            //fam.AddMember(new Person("Gosho", 4));
            //fam.AddMember(new Person("Annie", 5));
            //
            //Console.WriteLine(fam.GetOldestMember().Name + " " + fam.GetOldestMember().Age);
            //
            //Family fam2 = new Family();
            //fam2.AddMember(new Person("Steve", 10));
            //fam2.AddMember(new Person("Christopher", 15));
            //fam2.AddMember(new Person("Annie", 4));
            //fam2.AddMember(new Person("Ivan", 35));
            //fam2.AddMember(new Person("Maria", 34));
            //
            //Console.WriteLine(fam2.GetOldestMember().Name + " " + fam2.GetOldestMember().Age);
        }

        private static void Persons()
        {
            //Task 1,2
            string[] inputArgs = Console.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (inputArgs.Length == 0)
            {
                Person p = new Person();
                Console.WriteLine($"{p.Name} {p.Age}");
            }
            else if (inputArgs.Length == 1)
            {
                string arg = inputArgs[0];
                int age = -1;

                if (int.TryParse(arg, out age))
                {
                    Person p = new Person(age);
                    Console.WriteLine($"{p.Name} {p.Age}");
                }
                else
                {
                    Person p = new Person(arg);
                    Console.WriteLine($"{p.Name} {p.Age}");
                }
            }
            else if (inputArgs.Length == 2)
            {
                string name = inputArgs[0];
                int age = int.Parse(inputArgs[1]);
                Person p = new Person(name, age);
                Console.WriteLine($"{p.Name} {p.Age}");
            }           
        }
    }
}

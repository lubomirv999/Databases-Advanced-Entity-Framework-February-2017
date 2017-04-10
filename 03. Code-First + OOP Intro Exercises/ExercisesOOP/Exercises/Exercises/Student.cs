using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class Student
    {
        //Fields
        public static int count;

        //Constructor
        public Student(string name)
        {
            this.Name = name;
            count++;
        }

        //Properies
        public string Name { get; set; }

    }
}

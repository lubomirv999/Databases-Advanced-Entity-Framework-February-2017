using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class Person
    {
        //Task 1,2

        //Fields
        private string name;

        private int age;

        //Constructors
        public Person() : this ("No name", 1) {}

        public Person(int age) : this ("No name", age) {}

        public Person(string name) : this(name, 1) {}

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        //Properies
        public string Name {
            get
            {
                return this.name;
            }               
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid Name!");
                }

                this.name = value;
            }
        }

        public int Age
        { get
          {
                return this.age;
          }
          set
          {
                if(value < 0)
                {
                    throw new ArgumentException("Invalid Age!");
                }

                this.age = value;
          }
        }
    }
}

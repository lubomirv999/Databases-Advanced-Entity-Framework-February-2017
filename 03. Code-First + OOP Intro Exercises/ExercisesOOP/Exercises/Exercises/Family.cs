using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    class Family
    {
        //Fields
        private List<Person> members;

        public Family()
        {
            this.members = new List<Person>();
        }

        //Methods
        public void AddMember(Person member)
        {
            this.members.Add(member);
        }

        public Person GetOldestMember()
        {
            return this.Members.OrderByDescending(m => m.Age).FirstOrDefault();
        }

        //Properties
        public List<Person> Members
        {
            get
            {
                return members;
            }
        }
    }
}

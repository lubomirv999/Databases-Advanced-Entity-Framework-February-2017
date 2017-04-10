using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises.Models
{
    class Doctor
    {
        public class Doctor
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Specialty { get; set; }

            public List<Visitation> Visitations { get; set; } = new List<Visitation>();

        }
    }
}

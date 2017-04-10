using Exercises.Models;
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
            var diagnose = new Diagnose
            {
                Name = "Aspirin",
                Comment = "Do it"
            };

            var mermerski = new Doctor
            {
                Name = "Mermerski",
                Specialty = "Bilki i pro4ie"
            };

            var visitation = new Visitation()
            {
                Doctor = mermerski,
                Date = DateTime.Now
            };

            mermerski.Visitations.Add(visitation);

            var medicament = new Medicament() { Name = "Laika" };
            var patient1 = new Patient()
            {
                FirstName = "Peter",
                LastName = "Petrov",
                Address = "Adress",
                Email = "Peter.Petrov@abv.bg",
                DateOfBirth = new DateTime(1999, 05, 04),
                Diagnoses = { diagnose, diagnose },
                Visitations = { visitation },
                Medicaments = { medicament },
                HasMedicalInsurance = false
            };

            var context = new HospitalContext();
            context.Patients.Add(patient1);
            context.SaveChanges();
        }
    }
}

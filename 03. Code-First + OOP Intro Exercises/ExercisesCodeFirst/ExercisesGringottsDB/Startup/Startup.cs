using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Startup
{
    class Startup
    {
        static void Main(string[] args)
        {			
			Models.WizardDeposits.WizzardDeposit dumbledore = new Models.WizardDeposits.WizzardDeposit()
            {
                FirstName = "Albus",
                LastName = "Dumbledore",
                Age = 150,
                MagicWandCreator = "Antioch Peverell",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 20000.24m,
                DepositCharge = 0.2m,
                IsDepositExpired = false
            };

            var context = new GringottsContext();
            context.WizzardDeposits.Add(dumbledore);
            context.SaveChanges();
        }
    }
}

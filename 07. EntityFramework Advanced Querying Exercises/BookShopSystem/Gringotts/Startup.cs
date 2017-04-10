namespace Gringotts
{
    using Gringotts.Data;
    using System;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            GringottsContext context = new GringottsContext();

            //Task 19
            //OllivanderFamilyDepositSum(context);

            //Task 20
            //DepositsGroupsAndTheirSum(context);

        }

        private static void DepositsGroupsAndTheirSum(GringottsContext context)
        {
            //Task 20
            context.WizzardDeposits.Where(w => w.MagicWandCreator == "Ollivander family").GroupBy(w => w.DepositGroup).Where(d => d.Sum(w => w.DepositAmount.Value) < 150000).OrderByDescending(d => d.Sum(w => w.DepositAmount)).ToList().ForEach(d =>
            {
                Console.WriteLine($"{d.Key} - {d.Sum(w => w.DepositAmount)}");
            });
        }

        private static void OllivanderFamilyDepositSum(GringottsContext context)
        {
            //Task 19
            context.WizzardDeposits.Where(w => w.MagicWandCreator == "Ollivander family").GroupBy(w => w.DepositGroup).ToList().ForEach(d =>
            {
                Console.WriteLine($"{d.Key} - {d.Sum(w => w.DepositAmount)}");
            });
        }
    }
}

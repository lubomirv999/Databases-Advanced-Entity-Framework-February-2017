namespace Startup
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public partial class GringottsContext : DbContext
    {
        public GringottsContext() : base("name=GringottsContext")
        {
        }

        public virtual DbSet<Models.WizardDeposits.WizzardDeposit> WizzardDeposits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.MagicWandCreator)
                .IsUnicode(false);

            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.DepositGroup)
                .IsUnicode(false);

            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.DepositAmount)
                .HasPrecision(8, 2);

            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.DepositInterest)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Models.WizardDeposits.WizzardDeposit>()
                .Property(e => e.DepositCharge)
                .HasPrecision(8, 2);
        }
    }
}
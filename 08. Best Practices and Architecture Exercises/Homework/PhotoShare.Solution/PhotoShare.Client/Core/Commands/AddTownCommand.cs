namespace PhotoShare.Client.Core.Commands
{
    using System;

    using PhotoShare.Service;

    public class AddTownCommand
    {
        private TownService townService;

        public AddTownCommand(TownService townService)
        {
            this.townService = townService;
        }

        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            if (this.townService.IsTownExisting(townName))
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            this.townService.AddTown(townName, country);

            return $"Town {townName} was added successfully!";
        }
    }
}

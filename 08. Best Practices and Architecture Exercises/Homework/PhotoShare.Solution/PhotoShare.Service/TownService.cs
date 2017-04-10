namespace PhotoShare.Service
{
    using System.Data.Entity;
    using System.Linq;

    using PhotoShare.Data;
    using PhotoShare.Models;

    public class TownService
    {
        public void AddTown(string name, string countryName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town t = new Town();
                t.Name = name;
                t.Country = countryName;

                context.Towns.Add(t);
                context.SaveChanges();
            }
        }

        public bool IsTownExisting(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.Any(t => t.Name == townName);
            }
        }

        public Town GetByTownName(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.SingleOrDefault(t => t.Name == townName);
            }
        }
    }
}

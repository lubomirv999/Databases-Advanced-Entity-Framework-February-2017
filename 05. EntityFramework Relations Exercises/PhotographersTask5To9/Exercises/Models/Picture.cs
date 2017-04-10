
namespace Exercises.Models
{
    using System.Collections.Generic;

    //Task 6
    public class Picture
    {
        public Picture()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string FileSystemPath { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}

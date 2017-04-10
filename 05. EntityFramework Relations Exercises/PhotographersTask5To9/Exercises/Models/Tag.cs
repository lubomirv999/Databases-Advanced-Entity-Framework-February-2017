namespace Exercises.Models
{
    using Attributes;
    using System.Collections.Generic;

    public class Tag
    {
        //Task 7,9
        public Tag()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        [Tag]
        public string Label { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}

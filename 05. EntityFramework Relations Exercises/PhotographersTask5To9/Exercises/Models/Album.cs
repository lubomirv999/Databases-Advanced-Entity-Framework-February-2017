namespace Exercises.Models
{
    using System.Collections.Generic;

    public class Album
    {
        //Task 6,7,9    

        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();
            this.Photographers = new HashSet<Photographer>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string BackgroundColor { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Photographer> Photographers { get; set; }

        public int OwnerId { get; set; }
    }
}

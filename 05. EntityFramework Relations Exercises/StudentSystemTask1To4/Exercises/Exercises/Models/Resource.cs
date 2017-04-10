namespace Exercises.Models
{
    using Enums;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Resource
    {
        public Resource()
        {
            this.Licences = new HashSet<Licence>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ResourceType ResourceType { get; set; }

        [Required]
        public string Url { get; set; }

        public Cource Course { get; set; }

        public virtual ICollection<Licence> Licences { get; set; }
    }
}

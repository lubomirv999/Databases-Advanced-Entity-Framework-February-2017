namespace Exercises.Models
{
    using Exercises.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Homework
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public ContentType ContentType { get; set; }

        public DateTime SubmissionDate { get; set; }

        public Student Student { get; set; }

        public Cource Course { get; set; }
    }
}

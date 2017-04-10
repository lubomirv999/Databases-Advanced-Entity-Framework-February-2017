namespace Exercises.Migrations
{
    using Enums;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Exercises.Data.StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Exercises.Data.StudentSystemContext context)
        {
            Student student = new Student()
            {
                Name = "Dragan",
                RegistrationDate = new DateTime(2017, 3, 1),
                BirthDate = new DateTime(1988, 6, 23)
            };

            Student student2 = new Student()
            {
                Name = "Pesho",
                RegistrationDate = new DateTime(2017, 3, 1),
                BirthDate = new DateTime(1990, 8, 10)
            };

            Student student3 = new Student()
            {
                Name = "Kiro",
                RegistrationDate = new DateTime(2017, 3, 1),
                BirthDate = new DateTime(1994, 3, 7)
            };

            context.Students.AddOrUpdate(s => s.Name, student);
            context.Students.AddOrUpdate(s => s.Name, student2);
            context.Students.AddOrUpdate(s => s.Name, student3);
            context.SaveChanges();

            Resource resource = new Resource()
            {
                Name = "Sample",
                ResourceType = ResourceType.Document,
                Url = "www.SampleUrl.bg"
            };

            Resource resource2 = new Resource()
            {
                Name = "Sample2",
                ResourceType = ResourceType.Presentation,
                Url = "www.SampleUrl2.bg"
            };

            Resource resource3 = new Resource()
            {
                Name = "Sample3",
                ResourceType = ResourceType.Video,
                Url = "www.SampleUrl3.bg"
            };

            context.Resources.AddOrUpdate(r => r.Name, resource);
            context.Resources.AddOrUpdate(r => r.Name, resource2);
            context.Resources.AddOrUpdate(r => r.Name, resource3);
            context.SaveChanges();

            Homework homework = new Homework()
            {
                Content = "Homework",
                ContentType = ContentType.Application,
                SubmissionDate = new DateTime(2017, 4, 8),
                Student = student
            };

            Homework homework2 = new Homework()
            {
                Content = "Homework2",
                ContentType = ContentType.Application,
                SubmissionDate = new DateTime(2017, 4, 8),
                Student = student2
            };

            Homework homework3 = new Homework()
            {
                Content = "Homework3",
                ContentType = ContentType.Application,
                SubmissionDate = new DateTime(2017, 3, 8),
                Student = student3
            };

            context.Homeworks.AddOrUpdate(h => h.Content, homework);
            context.Homeworks.AddOrUpdate(h => h.Content, homework2);
            context.Homeworks.AddOrUpdate(h => h.Content, homework3);
            context.SaveChanges();

            Cource course = new Cource()
            {
                Name = "C# DB Advanced",
                Description = "DB Course for advanced students",
                StartDate = new DateTime(2017, 3, 8),
                EndDate = new DateTime(2017, 3, 8),
                Price = 200.00m,
                Students =
                {
                    student,
                    student2,
                    student3
                },
                HomeworkSubmissions =
                {
                    homework,
                    homework2,
                    homework3
                },
                Resources =
                {
                    resource,
                    resource2,
                    resource3
                }
            };

            context.SaveChanges();
        }
    }
}

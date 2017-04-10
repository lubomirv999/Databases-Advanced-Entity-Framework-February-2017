namespace Exercises
{
    using Exercises.Data;
    using Models;
    using System.Linq;
    using System;

    class Startup
    {
        static void Main(string[] args)
        {
            //Some of the properties are required by default so i skipped the [Required] Attribute
            //See Data, Enums, Migrations, Models for the solutions 

            StudentSystemContext context = new StudentSystemContext();

            //Task 3.1
            //ListAll(context);

            //Task 3.2
            //ListAllCourses(context);

            //Task 3.3
            //ListAllCourses5(context);

            //Task 3.4
            //ListAllCoursesByDate(context);

            //Task 3.5
            //StudentsAndCourses(context);
        }

        private static void StudentsAndCourses(StudentSystemContext context)
        {
            //Task 3.5
            foreach (var student in context.Students
                            .OrderByDescending(x => x.Cources.Sum(z => z.Price))
                            .ThenByDescending(x => x.Cources.Count)
                            .ThenBy(x => x.Name))
            {
                Console.WriteLine($"{student.Name}\nNumber of courses: {student.Cources.Count}" +
                                  $"\nTotal price: {student.Cources.Sum(c => c.Price)}\nAverage price:{student.Cources.Average(c => c.Price)}");
            }
        }

        private static void ListAllCoursesByDate(StudentSystemContext context)
        {
            context.Cources
                            .Where(c => c.StartDate < DateTime.Now && DateTime.Now < c.EndDate)
                            .OrderByDescending(c => c.Students.Count)
                            .ToList()
                            .OrderByDescending(c => c.EndDate.Subtract(c.StartDate).Days)
                            .ToList()
                            .ForEach(
                                c =>
                                    Console.WriteLine(
                                        $"Course name: {c.Name}\nStart date: {c.StartDate}\nEnd date: {c.EndDate} " +
                                        $"\nDuration: {c.EndDate.Subtract(c.StartDate).Days}\nStudents enrolled: {c.Students.Count}"));
        }

        private static void ListAllCourses5(StudentSystemContext context)
        {
            //Task 3.3
            var courses = context.Cources.Where(c => c.Resources.Count() > 5)
                            .OrderByDescending(c => c.Resources.Count)
                            .ThenByDescending(c => c.StartDate)
                            .ToList();

            foreach (var cource in courses)
            {
                Console.WriteLine($"{cource.Name}: {cource.Resources} resources");
            }
        }

        private static void ListAllCourses(StudentSystemContext context)
        {
            //Task 3.2
            var courses = context.Cources
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"{course.Name} {course.Description}");

                var resources = context.Resources
                    .Where(r => r.Course.Name == course.Name)
                    .Select(r => r).ToList();

                foreach (var resource in resources)
                {
                    Console.WriteLine($"{resource.Id} {resource.Name} {resource.ResourceType} {resource.Url}");
                }
            }
        }

        private static void ListAll(StudentSystemContext context)
        {
            //Task 3.1

            var students = context.Students.ToList();

            foreach (var student in students)
            {
                Console.WriteLine(student.Name);

                var homework = context.Homeworks
                    .Where(h => h.Student.Name == student.Name)
                    .Select(h => h);

                foreach (var homeworkSubmission in homework)
                {
                    Console.WriteLine($"{homeworkSubmission.Content} - {homeworkSubmission.ContentType}");
                }
            }
        }
    }
}

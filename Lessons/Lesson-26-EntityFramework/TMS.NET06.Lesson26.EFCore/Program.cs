using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace TMSStudens
{
    class Program
    {
        static void Main(string[] args)
        {
            using var tms = new TMSContext();
            tms.Database.Migrate();

            //var ddl = tms.Database.GenerateCreateScript();

            //Console.WriteLine(ddl);

            //tms.Database.ExecuteSqlRaw(
            //    @"CREATE VIEW View_HomeworksCounts AS
            //        SELECT s.[Name], Count(s.[StudentId]) as StudentHomeworksCount
            //        FROM [dbo].[homeworks] h
            //        JOIN [dbo].[Students] s on s.[StudentId] = h.[NewStudentId]
            //        GROUP BY s.[Name]");

            //var shc = tms.Set<StudentHomeworksCount>().ToArrayAsync().Result;

            using (var context = new TMSContext())
            {
                //var students = context.Students
                //   .Where(b => b.Name.Contains("dm"))
                //   .ToList();


                //var student = context.Students.Single(b => b.StudentId == 1);

                //var homeworks = context.Homeworks
                //                    //.ToList()
                //                    //.AsEnumerable()
                //                    .OrderByDescending(hw => hw.Mark)
                //                    .Select(hw => new { Id = hw.HomeworkId, PR = GenerateUrl(hw.PullRequest) })
                //                    //.Where(hw => GenerateUrl(hw.PullRequest).Contains("pull"))
                //                    .ToList();

                //var homeworks = context.Homeworks.ToList();

                //var student = context.Students
                //.AsNoTracking()
                //.Include(s => s.Homeworks)
                //.ThenInclude(hw => hw.Reviewer)
                //.Include(s => s.Avatar)
                //.Include(s => s.Homeworks)
                //.ThenInclude(hw => hw.Tags)
                //.Single(b => b.StudentId == 6);

                //var result = context.Entry(student)
                //    .Collection(b => b.Homeworks)
                //    .Query()
                //    .Where(hw => hw.IsCompleted == true)
                //    .ToList();
                //    //.Count();
                //    //.Load(); // student.Homeworks.Count;

                //student.ShortBio = "Born to code";
                //context.SaveChanges();

                //var homework = student.Homeworks.First();
                //homework.Student = null;
                //context.SaveChanges();

                //homework = student.Homeworks.First();             

            }

            using (var ctx = new TMSContext())
            {
                var student = ctx.Students
                    .Include(s => s.Homeworks)
                    .Where(s => s.Homeworks.Count > 0)
                    .First();

                var homework = student.Homeworks.First();
                homework.Student = null;
                ctx.SaveChanges();
            }

            using (var ctx = new TMSContext())
            {
                // Fetch a person from database and change phone number
                var person = ctx.Students.Single(p => p.StudentId == 50);
                person.FullName = "Full Name 3";

                // Change the person's name in the database to simulate a concurrency conflict
                ctx.Database.ExecuteSqlRaw(
                    "UPDATE dbo.Students SET last_name = 'Jane4' WHERE StudentId = 50");

                var saved = false;
                while (!saved)
                {
                    try
                    {
                        // Attempt to save changes to the database
                        ctx.SaveChanges();
                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        foreach (var entry in ex.Entries)
                        {
                            if (entry.Entity is Student)
                            {
                                var proposedValues = entry.CurrentValues;
                                var databaseValues = entry.GetDatabaseValues();

                                foreach (var property in proposedValues.Properties)
                                {
                                    var proposedValue = proposedValues[property];
                                    var databaseValue = databaseValues[property];

                                    // TODO: decide which value should be written to database
                                    // proposedValues[property] = <value to be saved>;
                                }

                                // Refresh original values to bypass next concurrency check
                                entry.OriginalValues.SetValues(databaseValues);
                            }
                            else
                            {
                                throw new NotSupportedException(
                                    "Don't know how to handle concurrency conflicts for "
                                    + entry.Metadata.Name);
                            }
                        }
                    }
                }
            }
        }

        public static string GenerateUrl(string prId)
        {
            if (string.IsNullOrEmpty(prId))
                return "https://github.com/tms-net/NET06";

            return "https://github.com/tms-net/NET06/pull/" + prId;
        }
    }
}

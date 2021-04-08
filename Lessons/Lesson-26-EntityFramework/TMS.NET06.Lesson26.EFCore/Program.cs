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

                var student = context.Students
                        //.AsNoTracking()
                        //.Include(s => s.Homeworks)
                        //.ThenInclude(hw => hw.Reviewer)
                        //.Include(s => s.Avatar)
                        //.Include(s => s.Homeworks)
                        //.ThenInclude(hw => hw.Tags)
                        .Single(b => b.StudentId == 6);

                //var result = context.Entry(student)
                //    .Collection(b => b.Homeworks)
                //    .Query()
                //    .Where(hw => hw.IsCompleted == true)
                //    .ToList();
                //    //.Count();
                //    //.Load(); // student.Homeworks.Count;

                //student.ShortBio = "Born to code";
                //context.SaveChanges();

                var homework = student.Homeworks.First();
                homework.Student = null;
                context.SaveChanges();

                homework = student.Homeworks.First();
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

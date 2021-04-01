using Microsoft.EntityFrameworkCore;
using System;

namespace TMSStudens
{
    class Program
    {
        static void Main(string[] args)
        {
            var tms = new TMSContext();

            var ddl = tms.Database.GenerateCreateScript();

            Console.WriteLine(ddl);

            //tms.Database.ExecuteSqlRaw(
            //    @"CREATE VIEW View_HomeworksCounts AS
            //        SELECT s.[Name], Count(s.[StudentId]) as StudentHomeworksCount
            //        FROM [dbo].[homeworks] h
            //        JOIN [dbo].[Students] s on s.[StudentId] = h.[NewStudentId]
            //        GROUP BY s.[Name]");

            var shc = tms.Set<StudentHomeworksCount>().ToArrayAsync().Result;
        }
    }
}

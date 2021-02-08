using System;
using System.Collections.Generic;

namespace TMS.HomeWork.MVConsole.App
{
    public class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int GroupNumber { get; set; }
        public int SpecialtyCode { get; set; }
        public EducationType EducationType { get; set; }

        public IEnumerable<ExamResult> ExamResults { get; }
    }
}

﻿using System;

namespace TMS.HomeWork.MVConsole.App
{
    class Student
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int GroupNumber { get; set; }
        public int SpecialtyCode { get; set; }
        public EducationType EducationType { get; set; }
    }
}

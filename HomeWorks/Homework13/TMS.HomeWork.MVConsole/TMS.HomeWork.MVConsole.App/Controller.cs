using RandomNameGeneratorLibrary;
using System;
using TMS.Homework.MVConsole.UI;

namespace TMS.HomeWork.MVConsole.App
{
    class Controller
    {
        private UI _ui;

        Random rnd = new Random();

        public Controller(UI ui)
        {
            _ui = ui;
            var student = GetStudent();
            ui.View(student);
        }
        public Student GetStudent()
        {
            var personGenerator = new PersonNameGenerator();

            Student student = new Student()
            {
                Id = GenerateId(),
                FirstName = personGenerator.GenerateRandomFirstName(),
                LastName = personGenerator.GenerateRandomLastName(),
                DateOfBirth = GenerateDateOfBirth(),
                Gender = GenerateGender(),
                GroupNumber = GenerateGroupNumber(),
                SpecialtyCode = GenerateSpecialtyCode(),
                EducationType = GenerateEducationType(),
            };

            return student;
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString().Substring(0, 4);
        }
        private Gender GenerateGender()
        {
            int value = rnd.Next(0, 1);

            return value == 0 ? Gender.Male : Gender.Female;
        }

        private DateTime GenerateDateOfBirth()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rnd.Next(range));
        }
        private int GenerateGroupNumber()
        {
            int value = rnd.Next(00001, 99999);

            return value;
        }

        private int GenerateSpecialtyCode()
        {
            int value = rnd.Next(1000000, 9999999);

            return value;
        }

        private EducationType GenerateEducationType()
        {
            int value = rnd.Next(0, 1);

            return value == 0 ? EducationType.Budget : EducationType.Paid;
        }
    }
}
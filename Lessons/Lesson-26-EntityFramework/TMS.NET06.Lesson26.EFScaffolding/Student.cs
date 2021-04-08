using System;
using System.Collections.Generic;

#nullable disable

namespace TMS.NET06.Lesson26.EFScaffolding
{
    public partial class Student
    {
        public Student()
        {
            HomeWorks = new HashSet<HomeWork>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }

        public virtual ICollection<HomeWork> HomeWorks { get; set; }
    }
}

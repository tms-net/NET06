using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSStudens
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [DataType("nvarchar(150)")]
        public string Name { get; set; }

        [Column("last_name")]
        [MaxLength(300)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Homework> Homeworks { get; set; } // Collection Navigation Property
		public StudentAvatar Avatar { get; set; }

        public Address Address { get; set; }
    }

    public class StudentHomeworksCount
    {
        public string StudentName { get; set; }
        public int HomeworksCount { get; set; }
    }
}

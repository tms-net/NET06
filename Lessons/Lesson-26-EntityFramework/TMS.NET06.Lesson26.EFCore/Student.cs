using Microsoft.EntityFrameworkCore.Internal;
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
		private ICollection<Homework> _homeworks;

		public Student()
		{
            Homeworks = new List<Homework>();
		}

        private Student(Action<object, string> lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

		private Action<object, string> LazyLoader { get; }

        public int StudentId { get; set; }

        [Required]
        [DataType("nvarchar(150)")]
        public string Name { get; set; }

        [Column("last_name")]
        [MaxLength(300)]
        [ConcurrencyCheck]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<Homework> Homeworks
        {
            get => LazyLoader.Load(this, ref _homeworks);
            set => _homeworks = value;
        } // Collection Navigation Property

		public /*virtual*/ StudentAvatar Avatar { get; set; }

        public Address Address { get; set; }

		public string ShortBio { get; set; }

		public string FullName { get; set; }
	}

    public class StudentHomeworksCount
    {
        public string StudentName { get; set; }
        public int HomeworksCount { get; set; }
    }

}
